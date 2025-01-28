using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecadorService.Models {
    internal class Record  {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Check { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt{ get; set; }

        internal static Record FromDataReader(IDataReader reader) {
            var record = new Record {
                Id = Convert.ToInt32(reader["id"]),
                EmployeeId = Convert.ToInt32(reader["employee_id"]),
                Check = Convert.ToDateTime(reader["check"])
            };
            if (!reader.IsDBNull(reader.GetOrdinal("created_at")))
            {
                record.CreatedAt = Convert.ToDateTime(reader["created_at"]);
            }
            if (!reader.IsDBNull(reader.GetOrdinal("updated_at")))
            {
                record.UpdatedAt = Convert.ToDateTime(reader["updated_at"]);
            }
            return record;
        }
    }
}
