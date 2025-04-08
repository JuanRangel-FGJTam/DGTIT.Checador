using DGTIT.Checador.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Interfaces {
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> FindAll();
        Task<IEnumerable<Employee>> FindByGeneralDirection(int generalDirectionId);
        Task<IEnumerable<Employee>> FindByGeneralDirection(IEnumerable<int> generalDirectionIds);
        Task<Employee> FindById(int employeeId);
        Task<Employee> FindByEmployeeNumber(int employeeId);
        Task UpdateEmployee(Employee employee);
        
        /// <summary>
        ///  Create a new employee
        /// </summary>
        /// <param name="employee">Employee data</param>
        /// <returns></returns>
        Task<long> CreateEmployee(Employee employee);

        /// <summary>
        /// Create a new employee with a specific id
        /// </summary>
        /// <param name="employee">Employee data</param>
        /// <param name="employeeId">Id of the new employee</param>
        /// <returns></returns>
        Task<long> CreateEmployee(Employee employee, long employeeId);
    }
}
