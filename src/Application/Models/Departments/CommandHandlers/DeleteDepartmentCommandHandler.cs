using Application.Models.Department.Commands;
using Infrastructure.Repositories.Interfaces.IDepartmentRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Department.CommandHandlers
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Unit>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _departmentRepository.DeleteDepartmentAsync(request.DepartmentId);
            return Unit.Value;
        }
    }
}
