using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Quartz;
using log4net;
using System.Threading.Tasks;
using System.Configuration;
using ChecadorService.Services;

namespace ChecadorService.Jobs {
    internal class LogJob : IJob {

        private readonly NotifyService notifyService;
        private readonly ILog log;
        public LogJob(NotifyService service)
        {
            notifyService = service;
            log = LogManager.GetLogger(typeof(LogJob));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            log.Info("Sending the notification signal to the server.");
            await notifyService.Notify();
        }
    }
}
