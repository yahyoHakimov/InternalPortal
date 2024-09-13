using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Employee.Commands
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }  // Now using DepartmentId for dynamic assignment
        public int RoleId { get; set; }  // Now using DepartmentId for dynamic assignment
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
