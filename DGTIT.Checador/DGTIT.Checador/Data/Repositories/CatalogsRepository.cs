using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;

namespace DGTIT.Checador.Data.Repositories
{
    public class CatalogsRepository : ICatalogsRepository
    {
        private readonly string connectionString = String.Empty;
        public CatalogsRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["UsuariosDB"].ConnectionString;
        }


        public async Task<IEnumerable<GeneralDirection>> FindAllGeneralDirections()
        {
            var responseList = new List<GeneralDirection>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var query = @"SELECT id, name, abbreviation, created_at, updated_at FROM [dbo].[general_directions]";
                var command = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        responseList.Add(GeneralDirection.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }

        public async Task<IEnumerable<Direction>> FindAllDirections()
        {
            var responseList = new List<Direction>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var query = @"SELECT id, name, created_at, updated_at, general_direction_id FROM [dbo].[directions]";
                var command = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        responseList.Add(Direction.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }

        public async Task<IEnumerable<SubDirectorate>> FindAllSubdirectorates()
        {
            var responseList = new List<SubDirectorate>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var query = @"SELECT id, name, created_at, updated_at, direction_id FROM subdirectorates";
                var command = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        responseList.Add(SubDirectorate.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }
        
        public async Task<IEnumerable<Department>> FindAllDepartment()
        {
            var responseList = new List<Department>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var query = @"SELECT id, name, created_at, updated_at, subdirectorate_id FROM departments";
                var command = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        responseList.Add(Department.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }
    }
}
