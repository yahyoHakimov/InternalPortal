using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RolePermission.Queries
{
    public class GetAllRolesQuery : IRequest<IEnumerable<IdentityRole<int>>>
    {
    }
}
