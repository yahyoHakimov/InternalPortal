using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Announcements.Queries.AnnouncementQueries
{
    public class GetAnnouncementByIdQuery : IRequest<Announcement>
    {
        public int AnnouncementId { get; set; }
    }
}
