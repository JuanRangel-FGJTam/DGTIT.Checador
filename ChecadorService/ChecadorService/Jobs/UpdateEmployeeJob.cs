using ChecadorService.Models;
using ChecadorService.Services;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecadorService.Jobs
{
    internal class UpdateEmployeeJob : IJob
    {
        private EmployeeService EmployeeService1 { get; }
        private readonly ILog logger;

        public UpdateEmployeeJob(EmployeeService service)
        {
            this.EmployeeService1 = service;
            this.logger = LogManager.GetLogger(typeof(UpdateEmployeeJob));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.Info("Beggining job updated employees.");

            // * local employees
            IEnumerable<Employee> localEmployees = Array.Empty<Employee>();
            try
            {
                localEmployees = await EmployeeService1.GetLocalEmployees();
            }
            catch (Exception err)
            {
                logger.Error("Fail at getting the local employees from the DB.", err);
                return;
            }

            // * get the employees  that has changes
            IEnumerable<Employee> employees2Update = localEmployees.Where(item => item.FingerprintUpdatedAt > item.UpdatedAt);
            
            // * send the employees 
            foreach (var emp in employees2Update)
            {
                try
                {
                    await EmployeeService1.UpdateRemoteEmployee(emp);
                }
                catch (Exception err)
                {
                    logger.Error("Fail at update the employees.", err);
                    return;
                }
            }

            logger.Info("Finished job updated employees.");
        }
    }
}
