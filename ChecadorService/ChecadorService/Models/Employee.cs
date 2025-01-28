using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecadorService.Models {
    internal class Employee {
        public int Id { get; set; }
        public long GeneralDirectionId { get; set; }
        public long DirectionId { get; set; }
        public long SubdirectorateId { get; set; }
        public long DepartmentId { get; set; }
        public int PlantillaId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public byte[] Fingerprint { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? StatusId { get; set; }
        public bool Active { get; set; }
        public int  EmployeeNumber { get; set; }


        // * used for the local employee entity
        public DateTime? DeletedAt { get; set; }
        public DateTime FingerprintUpdatedAt { get; set; }

        /// <summary>
        /// Generate a employee model with the data from the remover server.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>A employee model</returns>
        public static Employee FromDataReaderServer(IDataReader reader)
        {
            var employee = new Employee();
            employee.Id = Convert.ToInt32(reader["id"]);
            employee.GeneralDirectionId = Convert.ToInt64(reader["general_direction_id"]);
            employee.DirectionId = Convert.ToInt64(reader["direction_id"]);
            employee.SubdirectorateId = Convert.ToInt64(reader["subdirectorate_id"]);
            employee.DepartmentId = Convert.ToInt64(reader["department_id"]);
            employee.PlantillaId = Convert.ToInt32(reader["plantilla_id"]);
            employee.Name = reader["name"].ToString();
            employee.Photo = reader["photo"].ToString();
            employee.CreatedAt = Convert.ToDateTime(reader["created_at"]);
            employee.UpdatedAt = Convert.ToDateTime(reader["updated_at"]);
            employee.StatusId = Convert.ToInt32(reader["status_id"]);
            employee.Active = Convert.ToBoolean(reader["active"]);
            employee.EmployeeNumber = Convert.ToInt32(reader["employee_number"]);
            employee.DeletedAt = null;
            employee.FingerprintUpdatedAt = employee.UpdatedAt;

            // Assuming reader is an instance of SqlDataReader
            if (!reader.IsDBNull(reader.GetOrdinal("employee_number")))
            {
                employee.EmployeeNumber = Convert.ToInt32(reader["employee_number"]);
            }
            else
            {
                employee.EmployeeNumber = int.Parse(reader["plantilla_id"].ToString().Substring(1));
            }


            // Assuming reader is an instance of SqlDataReader
            if (!reader.IsDBNull(reader.GetOrdinal("fingerprint")))
            {
                employee.Fingerprint = (byte[])reader["fingerprint"];
            }
            else
            {
                employee.Fingerprint =Array.Empty<byte>(); // Handle null values
            }

            return employee;
        }

        /// <summary>
        /// Generate a employee model with the data from the local server.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>A employee model</returns>
        public static Employee FromDataReaderLocal(IDataReader reader)
        {
            var employee = new Employee();
            employee.Id = Convert.ToInt32(reader["id"]);
            employee.GeneralDirectionId = Convert.ToInt64(reader["general_direction_id"]);
            employee.DirectionId = Convert.ToInt64(reader["direction_id"]);
            employee.SubdirectorateId = Convert.ToInt64(reader["subdirectorate_id"]);
            employee.DepartmentId = Convert.ToInt64(reader["department_id"]);
            employee.PlantillaId = Convert.ToInt32(reader["plantilla_id"]);
            employee.Name = reader["name"].ToString();
            employee.Photo = reader["photo"].ToString();
            employee.CreatedAt = Convert.ToDateTime(reader["created_at"]);
            employee.UpdatedAt = Convert.ToDateTime(reader["updated_at"]);
            employee.StatusId = Convert.ToInt32(reader["status_id"]);
            employee.Active = Convert.ToBoolean(reader["active"]);
            employee.EmployeeNumber = Convert.ToInt32(reader["employee_number"]);
            employee.FingerprintUpdatedAt = Convert.ToDateTime(reader["fingerprint_updated_at"]);
            employee.DeletedAt = reader.IsDBNull(reader.GetOrdinal("deleted_at"))
              ? (DateTime?) null
              : Convert.ToDateTime(reader["deleted_at"]);

            // Assuming reader is an instance of SqlDataReader
            if (!reader.IsDBNull(reader.GetOrdinal("fingerprint")))
            {
                employee.Fingerprint = (byte[])reader["fingerprint"];
            }
            else
            {
                employee.Fingerprint = Array.Empty<byte>(); // Handle null values
            }
            return employee;
        }
    }
}