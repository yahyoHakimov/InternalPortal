using Domain.Entities;
using MediatR;

namespace Application.Models.Announcements.Commands
{
    public class CreateAnnouncementCommand : IRequest<Announcement>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int PostedByEmployeeId { get; set; } // Now linking to EmployeeModel
        public string TargetAudience { get; set; } // Target roles or departments
    }
}
