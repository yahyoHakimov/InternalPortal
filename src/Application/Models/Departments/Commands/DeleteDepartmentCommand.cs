using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Department.Commands
{
    public class DeleteDepartmentCommand : IRequest<Unit>
    {
        public int DepartmentId { get; set; }
    }
}
