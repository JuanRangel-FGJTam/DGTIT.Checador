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
    internal class ProcuEmployeeRepository : IProcuEmployeeRepo
    {
        private readonly string connectionString = String.Empty;
        public ProcuEmployeeRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ProcuraduriaDB"].ConnectionString;
        }

        public async Task<ProcuEmployee> FindByEmployeeNumber(int employeeNumber)
        {
            ProcuEmployee responseEntity = null;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var query = @"SELECT
					    IDEMPLEADO,
					    NUMEMP,
					    NOMBRE,
					    APELLIDOPATERNO,
					    APELLIDOMATERNO,
					    IDESTADOEMPLEADO,
					    FECHA_NAC,
					    FECHA_ALTA,
					    FECHABAJA,
					    CURP,
                        RFC,
					    FOTO,
					    FECHAFOTO,
					    [ACTIVO] = Cast( (Case When IDESTADOEMPLEADO in (1,6,7,8) Then 1 Else 0 End) as bit),
					    Genero = s.Sexo,
                        IDAREA
				    From [dbo].[EMPLEADO] e
				    Inner Join [dbo].[SEXO] s on e.IDSEXO = s.IDSEXO
				    WHERE NUMEMP = @numEmpleado";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@numEmpleado", employeeNumber);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        responseEntity = ProcuEmployee.FromDataReader(reader);
                    }
                }
                sqlConnection.Close();
            }
            return responseEntity;
        }

        public async Task<IEnumerable<ProcuEmployee>> SearchByEmployeeNumber(int employeeNumber)
        {
            var responseList = new List<ProcuEmployee>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var query = @"SELECT
					    IDEMPLEADO,
					    NUMEMP,
					    NOMBRE,
					    APELLIDOPATERNO,
					    APELLIDOMATERNO,
					    IDESTADOEMPLEADO,
					    FECHA_NAC,
					    FECHA_ALTA,
					    FECHABAJA,
					    CURP,
                        RFC,
					    FOTO,
					    FECHAFOTO,
					    [ACTIVO] = Cast( (Case When IDESTADOEMPLEADO in (1,6,7,8) Then 1 Else 0 End) as bit),
					    Genero = s.Sexo,
                        IDAREA
				    From [dbo].[EMPLEADO] e
				    Inner Join [dbo].[SEXO] s on e.IDSEXO = s.IDSEXO
				    WHERE NUMEMP like CONCAT(@numEmpleado, '%')";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@numEmpleado", employeeNumber);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        responseList.Add(ProcuEmployee.FromDataReader(reader));
                    }
                }
                sqlConnection.Close();
            }
            return responseList;
        }
    }
}
