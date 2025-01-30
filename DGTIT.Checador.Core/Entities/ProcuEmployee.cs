using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Entities
{
    public class ProcuEmployee
    {
        public int Id { get; set; }
        public int NumeroEmpleado{ get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public byte[] Foto{ get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string CURP { get; set; }
        public DateTime? FechaFoto { get; set; }
        public bool Activo { get; set; }
        public string Genero { get; set; }
        public int AreaId { get; set; }

        public static ProcuEmployee FromDataReader(IDataReader reader)
        {
             var employee = new ProcuEmployee {
                Id = Convert.ToInt32(reader["IDEMPLEADO"]),
                NumeroEmpleado = Convert.ToInt32(reader["NUMEMP"]),
                Nombre = reader["NOMBRE"].ToString(),
                ApellidoPaterno = reader["APELLIDOPATERNO"].ToString(),
                ApellidoMaterno = reader["APELLIDOMATERNO"].ToString(),
                FechaNacimiento = Convert.ToDateTime(reader["FECHA_NAC"].ToString()),
                FechaAlta = Convert.ToDateTime(reader["FECHA_ALTA"].ToString()),
                CURP = reader["CURP"].ToString(),
                Activo = Convert.ToBoolean(reader["ACTIVO"]),
                Genero = reader["Genero"].ToString(),
                Foto = (!reader.IsDBNull(reader.GetOrdinal("FOTO")))
                    ? (byte[])reader["FOTO"]
                    : Array.Empty<byte>(),
                FechaFoto = (!reader.IsDBNull(reader.GetOrdinal("FECHAFOTO")))
                    ? (DateTime?)Convert.ToDateTime(reader["FECHAFOTO"])
                    : null,
                 FechaBaja = (!reader.IsDBNull(reader.GetOrdinal("FECHABAJA")))
                    ? (DateTime?)Convert.ToDateTime(reader["FECHABAJA"])
                    : null,
                 AreaId = Convert.ToInt32(reader["IDAREA"])
             };
            return employee;
        }
    }
}
