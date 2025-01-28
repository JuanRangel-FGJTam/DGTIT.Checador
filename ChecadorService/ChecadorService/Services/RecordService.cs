using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ChecadorService.Data.Repositories;
using ChecadorService.Models;
using log4net;
using Microsoft.Extensions.Logging;

namespace ChecadorService.Services {
    internal class RecordService : IRecordService {

        private readonly IRecordRepository recordRepository;
        private readonly IRecordServerRepository recordServer;
        private readonly ILog logger;

        public RecordService(IRecordRepository recordRepository, IRecordServerRepository recordServer)
        {
            this.logger = LogManager.GetLogger(typeof(RecordService));
            this.recordRepository = recordRepository;
            this.recordServer = recordServer;
        }

        public async Task<IEnumerable<Record>> GetLocalRecords()
        {
            try
            {
                IEnumerable<Record> records = await this.recordRepository.FindAll();
                return records;
            }
            catch(Exception err)
            {
                logger.Error("Fail at attempt to get the local records.", err);
                return Array.Empty<Record>();
            }
        }

        public async Task<IEnumerable<int>> SendRecord2Server(IEnumerable<Record> records)
        {
            logger.Info("Beggining send records to server.");
            var recordsSended = new List<int>();
            foreach(var record in records)
            {
                var recordId = record.Id;
                // * upload the records
                logger.Debug($"Sending record {recordId} to server.");
                var resp = await this.recordServer.AddRecord(record);
                if (resp > 0)
                {
                    recordsSended.Add(recordId);
                    logger.Debug("Removing record: " + recordId);
                    try {
                        await this.recordRepository.DeleteById(recordId);
                    }
                    catch (Exception err) {
                        logger.Error("Fail at attempt to remove the record: " + recordId, err);
                    }
                }
            }
            return recordsSended;
        }
    }
}
