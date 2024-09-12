using Application.Models.Employee.Commands;
using Infrastructure.Repositories.Interfaces.IEmployeeRepo;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Models.Employee.CommandHandlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null)
            {
                return Unit.Value; // Handle employee not found scenario
            }

            // Update employee details
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
            employee.Role = request.Role;
            employee.Department = request.Department;
            employee.JobTitle = request.JobTitle;
            employee.HireDate = request.HireDate;
            employee.ApplicationUserId = request.ApplicationUserId;

            await _employeeRepository.UpdateEmployeeAsync(employee);

            return Unit.Value;
        }
    }
}
