using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces.IAnnouncementRepo
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> GetAnnouncementByIdAsync(int announcementId);
        Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync();
        Task AddAnnouncementAsync(Announcement announcement);
        Task UpdateAnnouncementAsync(Announcement announcement);
        Task DeleteAnnouncementAsync(int announcementId);
    }
}
