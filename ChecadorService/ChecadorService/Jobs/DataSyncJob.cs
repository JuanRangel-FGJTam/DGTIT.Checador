using ChecadorService.Services;
using Microsoft.Win32;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecadorService.Jobs {
    internal class DataSyncJob : IJob {

        private EmployeeService EmployeeService1 {get;} 

        public DataSyncJob(EmployeeService service)
        {
            this.EmployeeService1 = service;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // * server employees
            var employees = await EmployeeService1.GetServerEmployees();

            // * local employees
            var localEmployees = await EmployeeService1.GetLocalEmployees(includeDeleted: true);
            
            // * update
             var updated = await EmployeeService1.UpdateLocalEmployees(employees, localEmployees);
        }
    }
}
