using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Employee.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeModel>
    {
        public int EmployeeId { get; set; }
    }
}
