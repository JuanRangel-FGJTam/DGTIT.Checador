using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;

namespace DGTIT.Checador.Services {
    internal class ChecadorService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IRecordRepository recordRepository;

        public ChecadorService(IEmployeeRepository employeeRepo, IRecordRepository recordRepo)
        {
            this.employeeRepository = employeeRepo;
            this.recordRepository = recordRepo;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await employeeRepository.FindAll();
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> generalDirectionIds)
        {
            return await employeeRepository.FindByGeneralDirection(generalDirectionIds);
        }

        public async Task<DateTime> CheckInEmployee(int employeeNumber)
        {
            var serverTime = DateTime.Now;

            // * Get the employee id
            var employee = await this.employeeRepository.FindByEmployeeNumber(employeeNumber) 
                    ?? throw new KeyNotFoundException($"The employee with number '{employeeNumber}' was not found");

            // * make the check record
            var addedResults = await this.recordRepository.AddRecord( new  Record {
                EmployeeId = employee.Id,
                Check = serverTime,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            if(addedResults<=0)
            {
                throw new Exception("Can't stored the record on the database.");
            }
            return serverTime;
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
            throw new NotImplementedException();
            //// * search for the employee in the checadorDB
            //var employeeChec = this.usuariosDBEntities.employees.FirstOrDefault( item => item.employee_number == employeeNumber);

            //// * if there is not record in the checadorDB, generate a new one
            //if(employeeChec == null && makeRecordIfNotExist)
            //{
            //    // * retrive employee from procu DB (RH)
            //    var employeeRH = this.procuraduriaEntities.EMPLEADO.FirstOrDefault(item => item.NUMEMP == employeeNumber);
            //    if (employeeRH == null)
            //    {
            //        throw new KeyNotFoundException("The employee is not registered on the system.");
            //    }

            //    // * make the new employee and save them
            //    var _plantillaId = employeeNumber + 100000;

            //    employeeChec = new employee
            //    {
            //        plantilla_id = _plantillaId,
            //        name = $"{employeeRH.NOMBRE} {employeeRH.APELLIDO}",
            //        general_direction_id = 1, /* 1 => Desconocido */
            //        direction_id = 1, /* 1 => Desconocido */
            //        subdirectorate_id = 1, /* 1 => Desconocido */
            //        department_id = 1 /* 1 => Desconocido */,
            //        created_at = DateTime.Now,
            //        updated_at = DateTime.Now,
            //        fingerprint = Array.Empty<byte>(),
            //        employee_number = employeeNumber,
            //        status_id = 1,
            //        active = true
            //    };
            //    this.usuariosDBEntities.employees.Add(employeeChec);
            //    this.usuariosDBEntities.SaveChanges();
            //}

            //if (employeeChec == null && !makeRecordIfNotExist)
            //{
            //    throw new KeyNotFoundException("The employee is not registered on the system.");
            //}

            //return employeeChec;
            
        }

        public employee UpdateEmployee(employee emp)
        {
            throw new NotImplementedException();
            //emp.updated_at = DateTime.Now;
            //this.usuariosDBEntities.employees.AddOrUpdate(emp);
            //this.usuariosDBEntities.SaveChanges();
            //return emp;
        }

    }
}
