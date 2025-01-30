using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGTIT.Checador.Core.Entities;

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

        public static EmployeeViewModel FromEntity(ProcuEmployee entity)
        {
            var item = new EmployeeViewModel();
            item.Id = entity.Id;
            item.NumEmpleado = entity.NumeroEmpleado;
            item.Nombre = entity.Nombre;
            item.Paterno = entity.ApellidoPaterno;
            item.Materno = entity.ApellidoMaterno;
            item.Curp = entity.CURP;
            item.AreaId = entity.AreaId;
            return item;
        }
    }
}
