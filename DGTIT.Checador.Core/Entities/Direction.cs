using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Entities
{
    public class Direction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int GeneralDirectionId { get; set; }

        public static Direction FromDataReader(IDataReader reader   )
        {
            var entity = new Direction {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["name"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["created_at"].ToString()),
                UpdatedAt = Convert.ToDateTime(reader["updated_at"].ToString()),
                GeneralDirectionId = Convert.ToInt32(reader["general_direction_id"])
            };
            return entity;
        }
    }
}
