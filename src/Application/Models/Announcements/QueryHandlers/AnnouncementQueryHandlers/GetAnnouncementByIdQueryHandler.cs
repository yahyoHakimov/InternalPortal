using Application.Models.Announcements.Queries.AnnouncementQueries;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces.IAnnouncementRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Announcements.QueryHandlers.AnnouncementQueryHandlers
{
    public class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, Announcement>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public GetAnnouncementByIdQueryHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<Announcement> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            return await _announcementRepository.GetAnnouncementByIdAsync(request.AnnouncementId);
        }
    }
}
