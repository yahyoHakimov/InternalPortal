using Application.Models.Announcements.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Models.Announcements.CommandHandlers
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, Announcement>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public UpdateAnnouncementCommandHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<Announcement> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = await _announcementRepository.GetAnnouncementByIdAsync(request.AnnouncementId);

            if (announcement == null)
            {
                return null; // Or handle a not-found scenario appropriately
            }

            // Update the announcement details
            announcement.Title = request.Title;
            announcement.Content = request.Content;
            announcement.TargetAudience = request.TargetAudience;

            // Update the employee who posted the announcement
            announcement.PostedByEmployeeId = request.PostedByEmployeeId;

            await _announcementRepository.UpdateAnnouncementAsync(announcement);

            return announcement;
        }
    }
}
