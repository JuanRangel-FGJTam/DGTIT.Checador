using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using log4net;
using Quartz;
using ChecadorService.Models;
using ChecadorService.Services;

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
            IEnumerable<Employee> employees = Array.Empty<Employee>();
            try
            {
                employees = await EmployeeService1.GetServerEmployees();
            }
            catch(Exception err)
            {
                logger.Error("Fail at getting the employees from the server.", err);
                return;
            }
            
            // * local employees
            IEnumerable<Employee> localEmployees = Array.Empty<Employee>();
            try {
                localEmployees = await EmployeeService1.GetLocalEmployees(includeDeleted: true);
            }
            catch (Exception err) {
                logger.Error("Fail at getting the local employees from the DB.", err);
                return;
            }

            // * update
            logger.Info("Update the local employees");
            try {
                var updated = await EmployeeService1.UpdateLocalEmployees(employees, localEmployees);
            }
            catch (Exception err) {
                logger.Error("Fail at update the employees.", err);
                return;
            }

            logger.Info("Finished job sync employees.");
        }
    }
}
