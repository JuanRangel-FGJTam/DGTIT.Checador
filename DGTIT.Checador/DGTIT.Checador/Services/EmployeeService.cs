using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;
using DGTIT.Checador.Data.Repositories;
using DGTIT.Checador.Utilities;

namespace DGTIT.Checador.Services
{
    internal class EmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmployeeRepository centralEmployeeRepository;
        private readonly IProcuEmployeeRepo procuEmployeeRepo;

        public EmployeeService()
        {
            this.procuEmployeeRepo = new ProcuEmployeeRepository();
            this.centralEmployeeRepository = new SQLCentralEmployeeRepository();
            this.employeeRepository = new SQLClientEmployeeRepository();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await employeeRepository.FindAll();
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<int> generalDirectionIds)
        {
            return await employeeRepository.FindByGeneralDirection(generalDirectionIds);
        }

        /// <summary>
        ///     attempt to get the employee from the checadorDB and if is not registered make a new one with data of the RH.        
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <param name="makeRecordIfNotExist">sets if the employee not exist on checadorDB make the record </param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">The employee is not found</exception>
        public async Task<Employee> GetEmployee(int employeeNumber, bool makeRecordIfNotExist = true)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();

            //// * search for the employee in the local DB
            //var employee = await this.employeeRepository.FindByEmployeeNumber(employeeNumber);

            //// * if there is not record in the local DB, generate a new one
            //if (employee == null && makeRecordIfNotExist)
            //{
            //    // * retrive employee from procu DB (RH)
            //    var employeeRH = await this.procuEmployeeRepo.FindByEmployeeNumber(employeeNumber);
            //    if (employeeRH == null)
            //    {
            //        throw new KeyNotFoundException("The employee is not registered on the system.");
            //    }

            //    // * make the new employee and save them
            //    var _plantillaId = employeeNumber + 100000;

            //    employee = new Employee {
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
            //    this.usuariosDBEntities.employees.Add(employee);
            //    await this.usuariosDBEntities.SaveChangesAsync();
            //}

            //if (employee == null && !makeRecordIfNotExist)
            //{
            //    throw new KeyNotFoundException("The employee is not registered on the system.");
            //}

            //return employee;
        }

        public async Task<Employee> GetEmployee(int employeeNumber)
        {
            var employee = await this.employeeRepository.FindByEmployeeNumber(employeeNumber);
            if (employee == null)  throw new KeyNotFoundException("The employee is not registered on the system.");
            return employee;
        }

        public async Task UpdateEmployee(Employee emp)
        {
            emp.FingerPrintUpdatedAt = DateTime.Now;
            await this.employeeRepository.UpdateEmployee(emp);
        }
    
        /// <summary>
        ///  Attempt to clone or create the employee from the central DB to the local DB
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">The employee is not found</exception>
        public async Task<Employee> CreateEmployee(int employeeNumber)
        {
            // * Chek if the employee exist on the cental DB
            Employee employee = await this.centralEmployeeRepository.FindByEmployeeNumber(employeeNumber);
            if(employee != null)
            {
                // clone the central DB employee record to the local DB
                await CloneEmployee(employee.Id);
                return await GetEmployee(employeeNumber);
            }

            // *  Retrive the employee data from RH
            var employeeRH = await this.procuEmployeeRepo.FindByEmployeeNumber(employeeNumber);
            if(employeeRH == null)
            {
                throw new KeyNotFoundException("The employee is not found on the RH database");
            }

            // attempt to create a new employee on central DB
            var firstGeneralDirectionId = CustomApplicationSettings.GetGeneralDirections().ToString().Split(';').Select(d => Convert.ToInt64(d)).ToList().FirstOrDefault();
            var newEmployee = new Employee() {
                GeneralDirectionId = (int) firstGeneralDirectionId,
                DirectionId = 0,
                SubdirectorateId = 0,
                DepartmentId = 0,
                Photo = string.Format("photos/{0}.jpg", employeeRH.RFC),
                PlantillaId = Convert.ToInt64(string.Format("1{0}", employeeNumber)),
                Name = string.Join(" ", employeeRH.Nombre, employeeRH.ApellidoPaterno, employeeRH.ApellidoMaterno),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                StatusId = 1,
                Active = true,
                EmployeeNumber = employeeNumber,
            };
            var newEmployeeId = await centralEmployeeRepository.CreateEmployee(newEmployee);

            // clone the central DB employee record to the local DB
            await CloneEmployee(newEmployeeId);

            return await GetEmployee(employeeNumber);
        }

        private async Task CloneEmployee(long employeeId)
        {
            var centralEmployee = await this.centralEmployeeRepository.FindById((int) employeeId) ?? throw new KeyNotFoundException("Employee not found");
            await this.employeeRepository.CreateEmployee(centralEmployee, centralEmployee.Id);
        }
    }
}
