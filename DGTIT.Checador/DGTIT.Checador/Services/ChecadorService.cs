using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    
    }
}
