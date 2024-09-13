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
    public class GetAllAnnouncementsQueryHandler : IRequestHandler<GetAllAnnouncementsQuery, IEnumerable<Announcement>>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public GetAllAnnouncementsQueryHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<IEnumerable<Announcement>> Handle(GetAllAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            return await _announcementRepository.GetAllAnnouncementsAsync();
        }
    }
}
