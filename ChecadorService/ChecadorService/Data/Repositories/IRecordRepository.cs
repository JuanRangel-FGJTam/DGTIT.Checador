using ChecadorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecadorService.Data.Repositories {
    internal interface IRecordRepository {
        Task<IEnumerable<Record>> FindAll();

        Task<IEnumerable<Record>> FindByGeneralDirection(int generalDirectionId);

        Task<Record> FindById(int recordId);

        Task DeleteById(int recordId);
    }
}
