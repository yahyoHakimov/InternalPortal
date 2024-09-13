using Application.Models.Employee.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces.IEmployeeRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Employee.CommandHandlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new EmployeeModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                JobTitle = request.JobTitle,
                HireDate = request.HireDate,
                ApplicationUserId = request.ApplicationUserId,
                DepartmentId = request.DepartmentId,
                RoleId = request.RoleId  // Set role here
            };

            await _employeeRepository.AddEmployeeAsync(employee);
            return employee.EmployeeId;
        }
    }
}
