using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces.IMeetingRepo
{
    public interface IMeetingRepository
    {
        Task<List<Meeting>> GetAllAsync();

        Task AddMeetAsync(Meeting meeting);

        Task UpdateMeetAsync(Meeting meeting);

        Task<Meeting> GetByIdAsync(Guid meetingId);

        Task DeleteMeetAsync(Guid meetingId);
    }
}
