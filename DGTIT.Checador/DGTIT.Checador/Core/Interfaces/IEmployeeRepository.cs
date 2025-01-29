using DGTIT.Checador.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Interfaces {
    internal interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> FindAll();
        Task<IEnumerable<Employee>> FindByGeneralDirection(int generalDirectionId);
        Task<IEnumerable<Employee>> FindByGeneralDirection(IEnumerable<int> generalDirectionIds);
        Task<Employee> FindById(int employeeId);
        Task<Employee> FindByEmployeeNumber(int employeeId);
    }
}
