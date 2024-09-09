using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IKpiRepository
    {
        Task<IEnumerable<KPI>> GetAllEmployees();
        Task<KPI> GetEmployeeById(int id);
        Task AddEmployee(KPI kpi);
        Task UpdateEmployee(KPI kpi);
        Task DeleteEmployee(int id);
    }
}
