using DGTIT.Checador.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Core.Interfaces
{
    public interface IProcuEmployeeRepo
    {
        Task<ProcuEmployee> FindByEmployeeNumber(int employeeNumber);
        Task<IEnumerable<ProcuEmployee>> SearchByEmployeeNumber(int employeeNumber);
    }
}
