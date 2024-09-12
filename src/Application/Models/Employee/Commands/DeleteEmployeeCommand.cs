using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Employee.Commands
{
    public class DeleteEmployeeCommand : IRequest<Unit> 
    {
        public int EmployeeId { get; set; }
    }
}
