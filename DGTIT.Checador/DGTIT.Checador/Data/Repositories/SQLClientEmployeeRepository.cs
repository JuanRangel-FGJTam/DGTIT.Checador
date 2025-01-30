using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;
using DPFP.Capture;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace DGTIT.Checador.Data.Repositories
{
    internal class SQLClientEmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString = String.Empty;
        public SQLClientEmployeeRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["UsuariosDB"].ConnectionString;
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
		                    [fingerprint_updated_at]
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
		                    [fingerprint_updated_at]
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
		                    [fingerprint_updated_at]
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
		                    [fingerprint_updated_at]
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
		                    [fingerprint_updated_at]
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
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var query = @"UPDATE [dbo].[employees]
                    SET	fingerprint = @fingerPrint, fingerprint_updated_at = @fingerPrintUpdatedAt
                    WHERE id = @employeeId;";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@employeeId", employee.Id);
                command.Parameters.AddWithValue("@fingerPrint", employee.Fingerprint);
                command.Parameters.AddWithValue("@fingerPrintUpdatedAt", employee.FingerPrintUpdatedAt);
                var r =  await command.ExecuteNonQueryAsync();
                sqlConnection.Close();
            }
        }
    }
}
