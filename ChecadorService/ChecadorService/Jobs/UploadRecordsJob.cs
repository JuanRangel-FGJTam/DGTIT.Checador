using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;
using ChecadorService.Services;
using ChecadorService.Models;

namespace ChecadorService.Jobs {
    internal class UploadRecordsJob : IJob {
        private readonly IRecordService recordService;
        private readonly ILog logger;

        public UploadRecordsJob(IRecordService service)
        {
            this.recordService = service;
            this.logger = LogManager.GetLogger(typeof(UploadRecordsJob));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.Info("Beggining update the records job");

            IEnumerable<Record> localRecords = Array.Empty<Record>();
            try {
                localRecords = await recordService.GetLocalRecords();
                logger.Info($"{localRecords.Count()} records to upload.");
            }catch(Exception err) {
                logger.Error("Fail at retrive the local records", err);
                return;
            }

            if(localRecords.Any())
            {
                try {
                    var uploaded = await recordService.SendRecord2Server(localRecords);
                    logger.Info($"{uploaded.Count()} records to uploaded.");
                }catch(Exception err) {
                    logger.Error("Fail at uploading the records to the server.", err);
                }
            }
            logger.Info("Finished update the records job");
        }
    }
}
