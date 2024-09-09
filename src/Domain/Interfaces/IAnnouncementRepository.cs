using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncements();
        Task<Announcement> GetAnnouncementById(int id);
        Task AddAnnouncement(Announcement announcement);
        Task UpdateAnnouncement(Announcement announcement);
        Task DeleteAnnouncement(int id);
    }
}
