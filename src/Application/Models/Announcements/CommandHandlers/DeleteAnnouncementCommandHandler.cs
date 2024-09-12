using Application.Models.Announcements.Commands;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Announcements.CommandHandlers
{
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, Unit>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public DeleteAnnouncementCommandHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<Unit> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            await _announcementRepository.DeleteAnnouncementAsync(request.AnnouncementId);
            return Unit.Value;
        }
    }

}
