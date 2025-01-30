using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int SubDirectorateId { get; set; }

        public static Department FromDataReader(IDataReader reader)
        {
            var entity = new Department {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["name"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["created_at"].ToString()),
                UpdatedAt = Convert.ToDateTime(reader["updated_at"].ToString()),
                SubDirectorateId = Convert.ToInt32(reader["subdirectorate_id"].ToString())
            };
            return entity;
        }
    }
}
