using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using ChecadorService.Models;

namespace ChecadorService.Data.Repositories {
    internal class SQLRecordRepository : IRecordRepository
    {
        public async Task<IEnumerable<Record>> FindAll() {
            var records = new List<Record>();
            var connectionString = ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ToString() ?? throw new ArgumentNullException("UsuariosDBLocal", "The local connection string is missing.");
            using (var sqlConnection = new SqlConnection(connectionString)) {
                await sqlConnection.OpenAsync();
                var query = "Select id, employee_id, [check], created_at, updated_at From [dbo].[records]";
                var command = new SqlCommand(query, sqlConnection);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        records.Add(Record.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return records;
        }

        public async Task<IEnumerable<Record>> FindByGeneralDirection(int generalDirectionId) {
            var records = new List<Record>();
            var connectionString = ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ToString() ?? throw new ArgumentNullException("UsuariosDBLocal", "The local connection string is missing.");
            using (var sqlConnection = new SqlConnection(connectionString)) {
                await sqlConnection.OpenAsync();
                var query = @"Select r.id, r.employee_id, r.[check], r.created_at, r.updated_at 
                     From [dbo].[records] r
                     Inner Join [dbo].[employees] e on r.employee_id = e.id and e.general_direction_id = @generalDirectionId";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@generalDirectionId", generalDirectionId);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        records.Add(Record.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return records;
        }

        public async Task<Record> FindById(int recordId) {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
        
        public async Task DeleteById(int recordId)
        {
            var records = new List<Record>();
            var connectionString = ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ToString() ?? throw new ArgumentNullException("UsuariosDBLocal", "The local connection string is missing.");
            using (var sqlConnection = new SqlConnection(connectionString)) {
                await sqlConnection.OpenAsync();
                var query = "Delete From [dbo].[records] Where id = @recordId";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@recordId", recordId);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        records.Add(Record.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
        }
    }
}
