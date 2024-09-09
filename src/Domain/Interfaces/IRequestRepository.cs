using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetAllRequests();
        Task<Lifecycle> GetRequestById(int id);
        Task AddRequest(Request request);
        Task UpdateRequest(Request request);
        Task DeleteRequest(int id);
    }
}
