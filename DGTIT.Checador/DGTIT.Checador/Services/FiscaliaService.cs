using DGTIT.Checador.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Services {
    public class FiscaliaService
    {
        private readonly UsuariosDBEntities usuariosDBEntities;
        private procuraduriaEntities1 procuraduriaEntities;

        public FiscaliaService(procuraduriaEntities1 contextProcu, UsuariosDBEntities contextCheca)
        {
            this.procuraduriaEntities = contextProcu;
            this.usuariosDBEntities = contextCheca;
        }
        
        public IEnumerable<EmployeeViewModel> SearchEmployees(string search)
        {
            
            var empleadosQuery = procuraduriaEntities.EMPLEADO.Where(item => item.NUMEMP != 0);
           
            if (!string.IsNullOrEmpty(search))
            {
                empleadosQuery = empleadosQuery.Where(item =>
                        item.NUMEMP.ToString().Contains(search)
                        //item.NOMBRE.Contains(search) ||
                        //item.APELLIDOPATERNO.Contains(search) ||
                        //item.APELLIDOMATERNO.Contains(search)
                );
            }

            // * map the employees for retrieving only the necessary columns
            var empleados = empleadosQuery.Take(25).ToList().Select(emp => EmployeeViewModel.FromEntity(emp)).ToList();

            //foreach (var emp in empleados)
            //{
            //    try
            //    {
            //        emp.Area = procuraduriaEntities.AREA.Find(emp.AreaId).AREA1;
            //    }
            //    catch (Exception) { }
            //}

            return empleados;
            
        }

        public Bitmap GetEmployeePhoto(int employeeNumber)
        {
            var foto = (procuraduriaEntities
                .EMPLEADO
                .Where(u => u.NUMEMP == employeeNumber)
                .Select(u => u.FOTO)
                .SingleOrDefault()
            );

            Bitmap bmp;
            using (var ms = new MemoryStream(foto))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }

    }
}
