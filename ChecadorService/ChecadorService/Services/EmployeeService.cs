using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using log4net;
using Quartz;
using ChecadorService.Models;
using ChecadorService.Utils;
using System.Data;
using ChecadorService.Data.Repositories;

namespace ChecadorService.Services {
    internal class EmployeeService {

        private readonly ILog logger;

        public EmployeeService() {
            this.logger = this.logger = LogManager.GetLogger(typeof(EmployeeService));
        }

        public async Task<IEnumerable<Employee>> GetServerEmployees()
        {
            logger.Debug("Attempt to get the employees from the server.");

            var response = new List<Employee>();
            try
            {
                // * general directions used to filter the employees we want to retrieve from the server.
                var generalDirections = CustomApplicationSettings.GetGeneralDirections();

                // * get the connection string
                string serverConnectionString = ConfigurationManager.ConnectionStrings["UsuariosDBEntities"].ConnectionString;
                if(String.IsNullOrEmpty(serverConnectionString))
                {
                    throw new ArgumentNullException("The UsuariosDBEntities connection string is null or empty.");
                }

                // * get the users
                using (var sqlConnection = new SqlConnection(serverConnectionString)) {
                    sqlConnection.Open();
                    var query = @"SELECT [id]
                          ,[general_direction_id]
                          ,[direction_id] = IsNull(direction_id, 0)
                          ,[subdirectorate_id] = IsNull(subdirectorate_id, 0)
                          ,[department_id] = IsNull(department_id, 0)
                          ,[plantilla_id]
                          ,[name]
                          ,[photo]
                          ,[fingerprint]
                          ,[created_at]
                          ,[updated_at]
                          ,[status_id] = IsNull([status_id],0)
                          ,[active]
                          ,[employee_number] = (CASE WHEN [employee_number] is null THEN SUBSTRING( Convert(varchar(max), [plantilla_id]), 1, LEN([plantilla_id])) ELSE [employee_number] END)
                        FROM [UsuariosDB].[dbo].[employees]
                        WHERE general_direction_id IN(SELECT Id FROM @GeneralDirectionIds)";

                    // Create a DataTable to hold the integer list
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Id", typeof(int));
                    foreach (string originalId in generalDirections.Split(';')) {
                        // Try to parse each value as an integer
                        if (int.TryParse(originalId, out int parsedInt)) {
                            dataTable.Rows.Add(parsedInt);
                        }
                    }
                    var command = new SqlCommand(query, sqlConnection);
                    command.Parameters.Add( new SqlParameter
                        {
                            ParameterName = "@GeneralDirectionIds",
                            SqlDbType = SqlDbType.Structured,
                            TypeName = "IntList", // Must match the TVP type name in SQL Server
                            Value = dataTable
                        }
                    );

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                response.Add(Employee.FromDataReaderServer(reader));
                            }
                            catch(Exception err)
                            {
                                logger.Error($"Error: Can't parse the employee from the reader, employeeData:[{reader.ToString()}]", err);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error: Can't get the employees from the server: {ex.Message}", ex);
            }

            return response;
        }

        public async Task<IEnumerable<Employee>> GetLocalEmployees(bool includeDeleted = false )
        {
            var response = new List<Employee>();

            // * general directions used to filter the employees we want to retrieve from the server.
            var generalDirections = CustomApplicationSettings.GetGeneralDirections();

            // * get the connection string
            string serverConnectionString = ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ConnectionString;
            if (String.IsNullOrEmpty(serverConnectionString)) {
                throw new ArgumentNullException("The UsuariosDBLocal connection string is null or empty.");
            }

            // * get the users
            using (var sqlConnection = new SqlConnection(serverConnectionString)) {
                sqlConnection.Open();
                var query = @"SELECT	[id],
		                    [general_direction_id],
		                    [direction_id],
		                    [subdirectorate_id],
		                    [department_id],
		                    [plantilla_id],
		                    [name],
		                    [photo],
		                    [fingerprint],
		                    [created_at],
		                    [updated_at],
		                    [status_id],
		                    [active],
		                    [employee_number],
		                    [deleted_at],
		                    [fingerprint_updated_at]
                    FROM [UsuariosDB].[dbo].[employees]
                    WHERE 1 = (CASE WHEN @generalDirection = '0' THEN 1 ELSE ( CASE WHEN general_direction_id in (SELECT VALUE FROM [dbo].[SplitString](@generalDirection,';')) THEN 1 ELSE 0 END ) END )
                        AND 1 = ( CASE WHEN @includeDelete = 1 THEN 1 ELSE (CASE WHEN [deleted_at] IS NULL THEN 1 ELSE 0 END) END) ";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@generalDirection", generalDirections.Trim(';'));
                command.Parameters.AddWithValue("@includeDelete", includeDeleted ?1 : 0);

                using (var reader = await command.ExecuteReaderAsync()) {
                    while (reader.Read()) {
                        try {
                            response.Add(Employee.FromDataReaderLocal(reader));
                        }
                        catch (Exception err) {
                            logger.Error($"Error: Can't parse the employee from the reader, employeeData:[{reader.ToString()}]", err);
                        }
                    }
                }
                sqlConnection.Close();
            }
            return response;
        }

        public async Task<IEnumerable<int>> UpdateLocalEmployees(IEnumerable<Employee> remoteEmployees, IEnumerable<Employee> localEmployees)
        {
            var employeeIds = new List<int>();

            foreach(var employee in remoteEmployees)
            {
                // * get localEmployee
                var localEmployee = localEmployees.FirstOrDefault(item => item.EmployeeNumber == employee.EmployeeNumber);
                if (localEmployee == null)
                {
                    await CreateLocalEmployee(employee);
                }
                else
                {
                    if(localEmployee.UpdatedAt < employee.UpdatedAt || localEmployee.DeletedAt != null)
                    {
                        await UpdateLocalEmployee(localEmployee.Id, employee);
                    }
                }
                employeeIds.Add(employee.EmployeeNumber);
            }

            // * delete the rest of employees in the localdb that are not present from the server
            var employees2NotDelete = (await GetLocalEmployees()).Where(item => employeeIds.Contains(item.EmployeeNumber))
                .Select(item => item.Id)
                .ToList();

            await this.DeleteLocalEmployees(employees2NotDelete);

            return employeeIds;
        }

        /// <summary>
        /// update the local employee
        /// </summary>
        /// <param name="employeeId">local employee id</param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateLocalEmployee(int employeeId, Employee employee)
        {
            logger.Info($"Updated loca employee with number: '{employee.EmployeeNumber}'.");
            
            // * get the connection string
            string serverConnectionString = ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ConnectionString;
            if (String.IsNullOrEmpty(serverConnectionString)) {
                throw new ArgumentNullException("The UsuariosDBLocal connection string is null or empty.");
            }

            // * get the users
            using (var sqlConnection = new SqlConnection(serverConnectionString)) {
                sqlConnection.Open();
                var query = @"UPDATE [UsuariosDB].[dbo].[employees]
                      SET	[direction_id] = @dr,
		                    [subdirectorate_id] = @sd,
		                    [department_id] = @de,
		                    [name] = @na,
		                    [photo] = @ph,
		                    [fingerprint] = @fi,
		                    [updated_at] = @up,
		                    [status_id] = @st,
		                    [active] = @ac,
		                    [employee_number] = @en,
		                    [deleted_at] = null,
		                    [fingerprint_updated_at] = @up
                    WHERE id = @id ";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@id", employeeId);
                command.Parameters.AddWithValue("@dr", employee.DirectionId);
                command.Parameters.AddWithValue("@sd", employee.SubdirectorateId);
                command.Parameters.AddWithValue("@de", employee.DepartmentId);
                command.Parameters.AddWithValue("@na", employee.Name);
                command.Parameters.AddWithValue("@ph", employee.Photo);
                command.Parameters.AddWithValue("@fi", employee.Fingerprint);
                command.Parameters.AddWithValue("@up", employee.UpdatedAt);
                command.Parameters.AddWithValue("@st", employee.StatusId);
                command.Parameters.AddWithValue("@ac", employee.Active);
                command.Parameters.AddWithValue("@en", employee.EmployeeNumber);
               await command.ExecuteNonQueryAsync();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Store the employee and retrive the id if the new record
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateLocalEmployee(Employee employee)
        {
            logger.Info($"Create loca employee record with number: '{employee.EmployeeNumber}'.");

            // * get the connection string
            string serverConnectionString = ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ConnectionString;
            if (String.IsNullOrEmpty(serverConnectionString)) {
                throw new ArgumentNullException("The UsuariosDBLocal connection string is null or empty.");
            }

            // * store the employee
            int employeeId = 0;
            using (var sqlConnection = new SqlConnection(serverConnectionString)) {
                sqlConnection.Open();
                var query = @"
                    SET IDENTITY_INSERT [dbo].[employees] ON;
                    INSERT INTO [dbo].[employees](
                        [id],
	                    [employee_number],
	                    [general_direction_id],
	                    [direction_id],
	                    [subdirectorate_id],
	                    [department_id],
	                    [plantilla_id],
	                    [name],
	                    [photo],
	                    [fingerprint],
	                    [created_at],
	                    [updated_at],
	                    [status_id],
	                    [active],
	                    [fingerprint_updated_at])
                    VALUES(@id, @en, @gd, @dr, @sd, @de, @pla, @na, @ph, @fi, @cr, @up, @st, @ac, @fid );
                    SET IDENTITY_INSERT [dbo].[employees] OFF;
                    SELECT SCOPE_IDENTITY(); ";

                using (var command = new SqlCommand(query, sqlConnection))
                {
                    // Add parameters to avoid SQL injection
                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.Parameters.AddWithValue("@en", employee.EmployeeNumber);
                    command.Parameters.AddWithValue("@gd", employee.GeneralDirectionId);
                    command.Parameters.AddWithValue("@dr", employee.DirectionId);
                    command.Parameters.AddWithValue("@sd", employee.SubdirectorateId);
                    command.Parameters.AddWithValue("@de", employee.DepartmentId);
                    command.Parameters.AddWithValue("@pla", employee.PlantillaId);
                    command.Parameters.AddWithValue("@na", employee.Name);
                    command.Parameters.AddWithValue("@ph", employee.Photo);
                    command.Parameters.AddWithValue("@fi", employee.Fingerprint);
                    command.Parameters.AddWithValue("@st", employee.StatusId);
                    command.Parameters.AddWithValue("@ac", employee.Active);
                    command.Parameters.AddWithValue("@fid", employee.FingerprintUpdatedAt);
                    command.Parameters.AddWithValue("@cr", employee.CreatedAt);
                    command.Parameters.AddWithValue("@up", employee.UpdatedAt);

                    // Execute the query and get the last inserted ID
                    var result = await command.ExecuteScalarAsync();

                    // Return the inserted ID
                    employeeId = Convert.ToInt32(result);
                }
                sqlConnection.Close();
            }
            return employeeId;
        }

        /// <summary>
        /// Delete all employees except those who have passed through param.
        /// </summary>
        /// <param name="empoyeesId">Employees id to not delete</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task DeleteLocalEmployees(IEnumerable<int> empoyeesId)
        {
            logger.Info("Deleting local employees records.");
            // * get the connection string
            string serverConnectionString = ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ConnectionString;
            if (String.IsNullOrEmpty(serverConnectionString))
            {
                throw new ArgumentNullException("The UsuariosDBLocal connection string is null or empty.");
            }

            using (var sqlConnection = new SqlConnection(serverConnectionString))
            {
                sqlConnection.Open();
                var query = @"UPDATE [dbo].[employees] set deleted_at = GetDate() WHERE id NOT IN (SELECT VALUE FROM dbo.SplitString(@employeeIds, ';'))";

                using (var command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@employeeIds", string.Join(";", empoyeesId));
                    // Execute the query
                    var records = await command.ExecuteNonQueryAsync();
                    logger.Info($"Deleted {records} employees.");
                }
                sqlConnection.Close();
            }
        }

        public async Task UpdateRemoteEmployee(Employee employee)
        {
            logger.Info($"Updating the employee with number: '{employee.EmployeeNumber}'.");

            // * get the connection string
            string serverConnectionString = ConfigurationManager.ConnectionStrings["UsuariosDBEntities"].ConnectionString;
            if (String.IsNullOrEmpty(serverConnectionString))  throw new ArgumentNullException("The UsuariosDBLocal connection string is null or empty.");

            // * get the users
            using (var sqlConnection = new SqlConnection(serverConnectionString))
            {
                sqlConnection.Open();
                var query = @"UPDATE [dbo].[employees]
                      SET	[fingerprint] = @fi,
		                    [updated_at] = @up
                    WHERE id = @id ";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@id", employee.Id);
                command.Parameters.AddWithValue("@fi", employee.Fingerprint);
                command.Parameters.AddWithValue("@up", employee.FingerprintUpdatedAt);
                await command.ExecuteNonQueryAsync();
                sqlConnection.Close();
            }
        }

    }
}
