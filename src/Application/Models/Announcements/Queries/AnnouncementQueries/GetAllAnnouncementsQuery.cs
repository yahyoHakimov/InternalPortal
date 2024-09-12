using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Announcements.Queries.AnnouncementQueries
{
    public class GetAllAnnouncementsQuery : IRequest<IEnumerable<Announcement>>
    {
    }
}
