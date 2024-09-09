using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMeetingRepository
    {
        Task<IEnumerable<Meeting>> GetAllMeetings();
        Task<Lifecycle> GetMeetingById(int id);
        Task AddMeeting(Meeting meeting);
        Task UpdateMeeting(Meeting meeting);
        Task DeleteMeeting(int id);
    }
}
