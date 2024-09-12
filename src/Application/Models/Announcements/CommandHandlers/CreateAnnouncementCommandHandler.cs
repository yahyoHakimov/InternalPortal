using Application.Models.Announcements.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Models.Announcements.CommandHandlers
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Announcement>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public CreateAnnouncementCommandHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<Announcement> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = new Announcement
            {
                Title = request.Title,
                Content = request.Content,
                DatePosted = DateTime.UtcNow,
                PostedByEmployeeId = request.PostedByEmployeeId, // Link to the employee who posted
                TargetAudience = request.TargetAudience
            };

            await _announcementRepository.AddAnnouncementAsync(announcement);

            return announcement;
        }
    }
}
