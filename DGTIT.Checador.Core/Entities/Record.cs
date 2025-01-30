using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Entities {
    public class Record {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime Check { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
    }
}
