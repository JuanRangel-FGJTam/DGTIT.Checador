using ChecadorService.Services;
using log4net;
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
        private readonly ILog logger;

        public DataSyncJob(EmployeeService service)
        {
            this.EmployeeService1 = service;
            this.logger = LogManager.GetLogger(typeof(DataSyncJob));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.Info("Beggining job sync employees.");

            // * server employees
            logger.Info("Getting the server employees.");
            var employees = await EmployeeService1.GetServerEmployees();

            // * local employees
            var localEmployees = await EmployeeService1.GetLocalEmployees(includeDeleted: true);

            // * update
            logger.Info("Update the local employees");
            var updated = await EmployeeService1.UpdateLocalEmployees(employees, localEmployees);
            
            logger.Info("Finished job sync employees.");
        }
    }
}
