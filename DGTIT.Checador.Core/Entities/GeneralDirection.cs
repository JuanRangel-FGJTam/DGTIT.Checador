using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Entities
{
    public class GeneralDirection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static GeneralDirection FromDataReader(IDataReader reader)
        {
            var entity = new GeneralDirection {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["name"].ToString(),
                Abbreviation = reader["abbreviation"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["created_at"].ToString()),
                UpdatedAt = Convert.ToDateTime(reader["updated_at"].ToString()),
            };
            return entity;
        }
    }
}
