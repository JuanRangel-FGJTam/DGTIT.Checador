using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Entities {
    public class Employee {
        public int Id { get; set; }
        public int GeneralDirectionId { get; set; }
        public int DirectionId { get; set; }
        public int SubdirectorateId { get; set; }
        public int DepartmentId { get; set; }
        public long PlantillaId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public byte[] Fingerprint { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int StatusId { get; set; }
        public bool Active { get; set; }
        public int EmployeeNumber { get; set; }
        public DateTime FingerPrintUpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static Employee FromDataReader(IDataReader reader)
        {
            var employee = new Employee {
                Id = Convert.ToInt32(reader["id"]),
                GeneralDirectionId = Convert.ToInt32(reader["general_direction_id"]),
                DirectionId = Convert.ToInt32(reader["direction_id"]),
                SubdirectorateId = Convert.ToInt32(reader["subdirectorate_id"]),
                DepartmentId = Convert.ToInt32(reader["department_id"]),
                PlantillaId = Convert.ToInt32(reader["plantilla_id"]),
                Name = reader["name"].ToString(),
                Photo = reader["photo"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                UpdatedAt = Convert.ToDateTime(reader["updated_at"]),
                StatusId = Convert.ToInt32(reader["status_id"]),
                Active = Convert.ToBoolean(reader["active"]),
                EmployeeNumber = Convert.ToInt32(reader["employee_number"]),
                FingerPrintUpdatedAt = (!reader.IsDBNull(reader.GetOrdinal("fingerprint_updated_at")))
                    ? Convert.ToDateTime(reader["fingerprint_updated_at"])
                    : Convert.ToDateTime(reader["updated_at"]),
                Fingerprint = (!reader.IsDBNull(reader.GetOrdinal("fingerprint")))
                    ? (byte[])reader["fingerprint"]
                    : Array.Empty<byte>(),
                DeletedAt = (!reader.IsDBNull(reader.GetOrdinal("deleted_at")))
                    ? (DateTime?)Convert.ToDateTime(reader["deleted_at"])
                    : null
            };
            return employee;
        }
    }
}
