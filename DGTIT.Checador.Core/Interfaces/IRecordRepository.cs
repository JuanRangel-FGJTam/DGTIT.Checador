using DGTIT.Checador.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Interfaces {
    public interface IRecordRepository {
        Task<int> AddRecord(Record record);
    }
}
