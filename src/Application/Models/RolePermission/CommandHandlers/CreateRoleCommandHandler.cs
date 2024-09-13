using Application.Models.RolePermission.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RolePermission.CommandHandlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public CreateRoleCommandHandler(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new IdentityRole<int> { Name = request.RoleName };
            await _roleManager.CreateAsync(role);
            return role.Id;
        }
    }
}
