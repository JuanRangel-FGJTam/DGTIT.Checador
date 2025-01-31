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
        public LogJob(NotifyService service)
        {
            notifyService = service;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await notifyService.Notify();
        }
    }
}
