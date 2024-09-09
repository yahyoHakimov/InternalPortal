using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IKPIRepository
    {
        Task<IEnumerable<KPI>> GetKPIsForEmployee(int employeeId);
        Task AddKPI(KPI kpi);
        Task UpdateKPI(KPI kpi);
        Task DeleteKPI(int kpiId);
    }

}
