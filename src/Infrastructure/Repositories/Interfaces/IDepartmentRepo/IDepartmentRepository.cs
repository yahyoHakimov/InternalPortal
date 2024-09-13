using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces.IDepartmentRepo
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentModel>> GetAllDepartmentsAsync();
        Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId);
        Task AddDepartmentAsync(DepartmentModel department);
        Task UpdateDepartmentAsync(DepartmentModel department);
        Task DeleteDepartmentAsync(int departmentId);
    }
}
