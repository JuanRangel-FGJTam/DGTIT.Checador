using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;

namespace DGTIT.Checador.Data.Repositories {
    internal class SQLClientRecordRepository : IRecordRepository
    {
        private readonly string connectionString = String.Empty;
        public SQLClientRecordRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["UsuariosDB"].ConnectionString;
        }
        
        public  async Task<int> AddRecord(Record record)
        {
            var response = 0;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
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
