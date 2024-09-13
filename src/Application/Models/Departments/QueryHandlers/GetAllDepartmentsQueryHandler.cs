using Application.Models.Department.Queries;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces.IDepartmentRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Departments.QueryHandlers
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentModel>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentModel>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return await _departmentRepository.GetAllDepartmentsAsync();
        }
    }
}
