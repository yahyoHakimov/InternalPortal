using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RolePermission.Commands
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public int RoleId { get; set; }
    }
}
