using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGTIT.Checador.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int NumEmpleado{ get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno{ get; set; }
        public string Curp { get; set; }
        public int AreaId{ get; set; }
        public string Area { get; set; }

        public static EmployeeViewModel FromEntity(EMPLEADO entity)
        {
            var item = new EmployeeViewModel();
            item.Id = entity.IDEMPLEADO;
            item.Nombre = entity.NOMBRE;
            item.Paterno = entity.APELLIDOPATERNO;
            item.Materno = entity.APELLIDOMATERNO;
            item.Curp = entity.CURP;
            item.AreaId = entity.IDAREA??0;
            return item;
        }
    }
}
