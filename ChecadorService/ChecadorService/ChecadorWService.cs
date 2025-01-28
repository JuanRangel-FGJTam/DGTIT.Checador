using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using ChecadorService.Jobs;

namespace ChecadorService {
    internal class ChecadorWService {

        private IScheduler Scheduler { get; }

        public ChecadorWService(IScheduler scheduler) {
            this.Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }
            
        public void OnStart()
        {
            // * Define the jobs
            IJobDetail job1 = JobBuilder.Create<LogTimeJob>()
                .WithIdentity(typeof(LogTimeJob).Name, SchedulerConstants.DefaultGroup)
                .Build();

            IJobDetail job2 = JobBuilder.Create<DataSyncJob>()
                .WithIdentity(typeof(DataSyncJob).Name, SchedulerConstants.DefaultGroup)
                .Build();

            // * Create a trigger that fires on a cron schedule (every 30 minutes in this example)
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity(typeof(LogTimeJob).Name + "Trigger", SchedulerConstants.DefaultGroup)
                .WithCronSchedule("0 0/1 * * * ?")
                .ForJob(job1)
                .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity(typeof(DataSyncJob).Name + "Trigger", SchedulerConstants.DefaultGroup)
                .WithCronSchedule("0 0/1 * * * ?")
                .ForJob(job2)
                .Build();

            // *  Schedule the job with the trigger and start the scheduler
            Scheduler.ScheduleJob(job1, trigger1);
            Scheduler.ScheduleJob(job2, trigger2);
            Scheduler.Start();
        }

        public void OnStop() {
            Scheduler.Shutdown();
        }

        public void OnPaused() {
           Scheduler.PauseAll();
        }

        public void OnContinue() {
            Scheduler.ResumeAll();
        }

    }
}
