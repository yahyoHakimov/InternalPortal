using Application.Models.Departments.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces.IDepartmentRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Departments.CommandHandlers

{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new DepartmentModel
            {
                DepartmentName = request.DepartmentName
            };

            await _departmentRepository.AddDepartmentAsync(department);
            return department.DepartmentId;
        }
    }
}
