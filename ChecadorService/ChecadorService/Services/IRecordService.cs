using ChecadorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecadorService.Services {
    internal interface IRecordService {
        
        /// <summary>
        /// Retrive the records stored in the local DB
        /// </summary>
        /// <returns>Collection of records</returns>
        Task<IEnumerable<Record>> GetLocalRecords();

        /// <summary>
        /// Upload the local records to the sever
        /// </summary>
        /// <param name="records">Records to upload</param>
        /// <returns>Collection of the records id's uploaded succesfully</returns>
        Task<IEnumerable<int>> SendRecord2Server(IEnumerable<Record> records);
    }
}
