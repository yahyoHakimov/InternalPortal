using Application.Models.RolePermission.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RolePermission.QueriyHandlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<IdentityRole<int>>>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityRole<int>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return _roleManager.Roles.ToList();
        }
    }
}
