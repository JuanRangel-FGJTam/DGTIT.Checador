using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChecadorService.Models;

namespace ChecadorService.Data.Repositories {
    internal interface IRecordServerRepository
    {
        Task<int> AddRecord(Record record);
    }
}
