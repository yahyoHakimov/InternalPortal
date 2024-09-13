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
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }

            return Unit.Value;
        }
    }
}
