using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChecadorService.Models;

namespace ChecadorService.Data.Repositories {
    internal class RecordServerRepo : IRecordServerRepository {
        public async Task<int> AddRecord(Record record)
        {
            var response = 0;
            var connectionString = ConfigurationManager.ConnectionStrings["UsuariosDBEntities"].ToString() ?? throw new ArgumentNullException("UsuariosDBEntities", "The local connection string is missing.");
            using (var sqlConnection = new SqlConnection(connectionString)) {
                await sqlConnection.OpenAsync();
                var query = "INSERT INTO [dbo].[records]( [employee_id], [check], [created_at], [updated_at]) VALUES(@empId, @check, @created, GETDATE())";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@empId", record.EmployeeId);
                command.Parameters.AddWithValue("@check", record.Check);
                command.Parameters.AddWithValue("@created", record.CreatedAt);
                response = command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            return response;
        }
    }
}
