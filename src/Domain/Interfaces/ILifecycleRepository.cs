using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILifecycleRepository
    {
        Task<IEnumerable<Lifecycle>> GetAllLifecycles();
        Task<Lifecycle> GetLifecycleById(int id);
        Task AddLifecycle(Lifecycle lifecycle);
        Task UpdateLifecycle(Lifecycle lifecycle);
        Task DeleteLifecycle(int id);
    }
}
