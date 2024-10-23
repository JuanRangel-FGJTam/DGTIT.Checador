using DGTIT.Checador.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace DGTIT.Checador.Services {
    public class ChecadorService
    {
        private readonly UsuariosDBEntities usuariosDBEntities;
        private readonly procuraduriaEntities1 procuraduriaEntities;

        //private IEnumerable<employee> _cachedEmployees = Array.Empty<employee>();
        //private DateTime _lastCached = DateTime.Now;

        public ChecadorService( UsuariosDBEntities context, procuraduriaEntities1 procuraduriaContext)
        {
            this.usuariosDBEntities = context;
            this.procuraduriaEntities = procuraduriaContext;
        }

        public IEnumerable<employee> GetEmployees()
        {
            //if (_cachedEmployees.Any() && _lastCached >= DateTime.Now.Subtract(TimeSpan.FromMinutes(10))) {
            //    return this._cachedEmployees;
            //}

            // Update cache and timestamp
            var _cachedEmployees = this.usuariosDBEntities.employees
            .Where(item => item.fingerprint != null)
            .ToList();

            // this._lastCached = DateTime.Now; // Set the new cache time

            return _cachedEmployees;
        }

        /// <summary>
        ///     attempt to get the employee from the checadorDB and if is not registered make a new one with data of the RH.        
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <param name="makeRecordIfNotExist">sets if the employee not exist on checadorDB make the record </param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">The employee is not found</exception>
        public employee GetEmployee(int employeeNumber, bool makeRecordIfNotExist = true)
        {
            // * search for the employee in the checadorDB
            var employeeChec = this.usuariosDBEntities.employees.FirstOrDefault( item => item.employee_number == employeeNumber);

            // * if there is not record in the checadorDB, generate a new one
            if(employeeChec == null && makeRecordIfNotExist)
            {
                // * retrive employee from procu DB (RH)
                var employeeRH = this.procuraduriaEntities.EMPLEADO.FirstOrDefault(item => item.NUMEMP == employeeNumber);
                if (employeeRH == null)
                {
                    throw new KeyNotFoundException("The employee is not registered on the system.");
                }

                // * make the new employee and save them
                var _plantillaId = employeeNumber + 100000;

                employeeChec = new employee
                {
                    plantilla_id = _plantillaId,
                    name = $"{employeeRH.NOMBRE} {employeeRH.APELLIDO}",
                    general_direction_id = 1, /* 1 => Desconocido */
                    direction_id = 1, /* 1 => Desconocido */
                    subdirectorate_id = 1, /* 1 => Desconocido */
                    department_id = 1 /* 1 => Desconocido */,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                    fingerprint = Array.Empty<byte>(),
                    employee_number = employeeNumber,
                    status_id = 1,
                    active = true
                };
                this.usuariosDBEntities.employees.Add(employeeChec);
                this.usuariosDBEntities.SaveChanges();
            }

            if (employeeChec == null && !makeRecordIfNotExist)
            {
                throw new KeyNotFoundException("The employee is not registered on the system.");
            }

            return employeeChec;
            
        }

        public working_hours GetWorkinHours(int employeeNumber)
        {
            var empl = this.usuariosDBEntities.employees.FirstOrDefault(item => item.employee_number == employeeNumber);
            var workingHours = this.usuariosDBEntities.working_hours.FirstOrDefault(item => item.employee_id_ == empl.id);

            if( workingHours == null)
            {
                workingHours = new working_hours
                {
                    created_at = DateTime.Now,
                    employee_id_ = empl.id
                };
            }
            return workingHours;
        }

        public employee UpdateEmployee(employee emp)
        {
            emp.updated_at = DateTime.Now;
            this.usuariosDBEntities.employees.AddOrUpdate(emp);
            this.usuariosDBEntities.SaveChanges();
            return emp;
        }

        public working_hours UpdateWorkingHours(working_hours hours)
        {
            hours.updated_at = DateTime.Now;
            this.usuariosDBEntities.working_hours.AddOrUpdate(hours);
            this.usuariosDBEntities.SaveChanges();
            return hours;
        }

        public DateTime CheckInEmployee(int employeeNumber)
        {
            // * get the server time
            DateTime serverDate = this.usuariosDBEntities.Database.SqlQuery<DateTime>("SELECT getdate()").AsEnumerable().First();

            // * get the employee id
            var employee = this.GetEmployee(employeeNumber, false);
            
            // * make the check record
            var checkRecord = this.usuariosDBEntities.records.Add( new record()
            {
                employee_id = employee.id,
                check = serverDate,
                created_at = serverDate,
                updated_at = serverDate
            });
            usuariosDBEntities.SaveChanges();
            return serverDate;
        }

    }
}
