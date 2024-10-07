using DGTIT.Checador.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Services {
    public class ChecadorService
    {
        private readonly UsuariosDBEntities usuariosDBEntities;

        public ChecadorService( UsuariosDBEntities context)
        {
            this.usuariosDBEntities = context;
        }

        public employee GetEmployee(int employeeNumber)
        {
            var _plantillaId = employeeNumber + 100000;
            return this.usuariosDBEntities.employees.FirstOrDefault( item => item.plantilla_id == _plantillaId);
        }

        public working_hours GetWorkinHours(int employeeNumber)
        {
            var _plantillaId = employeeNumber + 100000;
            var empl = this.usuariosDBEntities.employees.FirstOrDefault(item => item.plantilla_id == _plantillaId);
            return this.usuariosDBEntities.working_hours.FirstOrDefault(item => item.employee_id_ == empl.id);

        }

    }
}
