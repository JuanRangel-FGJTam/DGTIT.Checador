using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;
using DPFP.Capture;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace DGTIT.Checador.Data.Repositories
{
    internal class SQLCentralEmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString = String.Empty;
        public SQLCentralEmployeeRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["UsuariosDBEntities"].ConnectionString;
        }

        public async Task<IEnumerable<Employee>> FindAll()
        {
            var responseList = new List<Employee>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
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
                            [fingerprint_updated_at] = null
                    FROM [UsuariosDB].[dbo].[employees]";
                var command = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        responseList.Add(Employee.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }

        public async Task<IEnumerable<Employee>> FindByGeneralDirection(int generalDirectionId)
        {
            var responseList = new List<Employee>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {   
                await sqlConnection.OpenAsync();
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
                            [fingerprint_updated_at] = null
                    FROM [UsuariosDB].[dbo].[employees]
                    WHERE [general_direction_id] = @generalDirectionId";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@generalDirectionId", generalDirectionId);
                using(SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while(reader.Read())
                    {
                        responseList.Add(Employee.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }

        public async Task<IEnumerable<Employee>> FindByGeneralDirection(IEnumerable<int> generalDirectionIds) {
            var responseList = new List<Employee>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
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
                            [fingerprint_updated_at] = null
                    FROM [UsuariosDB].[dbo].[employees]
                    WHERE [general_direction_id] in ( SELECT VALUE FROM string_split(@generalDirectionsIds, ';') )";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@generalDirectionsIds", string.Join(";", generalDirectionIds));
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        responseList.Add(Employee.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }

        public async Task<Employee> FindById(int employeeId) {
            Employee responseEntity = null;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
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
                            [fingerprint_updated_at] = null
                    FROM [UsuariosDB].[dbo].[employees]
                    WHERE id = @employeeId";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@employeeId", employeeId);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        responseEntity = Employee.FromDataReader(reader);
                    }
                }
                sqlConnection.Close();
            }
            return responseEntity;
        }
        
        public async Task<Employee> FindByEmployeeNumber(int employeeNumber)
        {
            Employee responseEntity = null;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
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
                            [fingerprint_updated_at] = null
                    FROM [UsuariosDB].[dbo].[employees]
                    WHERE employee_number = @employeeNumber";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@employeeNumber", employeeNumber);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        responseEntity = Employee.FromDataReader(reader);
                    }
                }
                sqlConnection.Close();
            }
            return responseEntity;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<long> CreateEmployee(Employee employee)
        {
            // * store the employee
            long employeeId = 0;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var query = @"
                    INSERT INTO [dbo].[employees](
                        [employee_number],
	                    [general_direction_id],
	                    [direction_id],
	                    [subdirectorate_id],
	                    [department_id],
	                    [plantilla_id],
	                    [name],
                        [photo],
	                    [created_at],
	                    [updated_at],
	                    [status_id],
	                    [active])
                    VALUES(@en, @gd, @dr, @sd, @de, @pla, @na, @ph, @cr, @up, @st, @ac);
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
                    command.Parameters.AddWithValue("@st", employee.StatusId);
                    command.Parameters.AddWithValue("@ac", employee.Active);
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

        public async Task<long> CreateEmployee(Employee employee, long employeeId)
        {
            // * store the employee
            using (var sqlConnection = new SqlConnection(connectionString))
            {
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
                        [photo]
	                    [created_at],
	                    [updated_at],
	                    [status_id],
	                    [active])
                    VALUES(@id, @en, @gd, @dr, @sd, @de, @pla, @na, @ph, @cr, @up, @st, @ac );
                    SET IDENTITY_INSERT [dbo].[employees] OFF;
                    SELECT SCOPE_IDENTITY(); ";

                using (var command = new SqlCommand(query, sqlConnection))
                {
                    // Add parameters to avoid SQL injection
                    command.Parameters.AddWithValue("@id", employeeId);
                    command.Parameters.AddWithValue("@en", employee.EmployeeNumber);
                    command.Parameters.AddWithValue("@gd", employee.GeneralDirectionId);
                    command.Parameters.AddWithValue("@dr", employee.DirectionId);
                    command.Parameters.AddWithValue("@sd", employee.SubdirectorateId);
                    command.Parameters.AddWithValue("@de", employee.DepartmentId);
                    command.Parameters.AddWithValue("@pla", employee.PlantillaId);
                    command.Parameters.AddWithValue("@na", employee.Name);
                    command.Parameters.AddWithValue("@ph", employee.Photo);
                    command.Parameters.AddWithValue("@st", employee.StatusId);
                    command.Parameters.AddWithValue("@ac", employee.Active);
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
    }
}
