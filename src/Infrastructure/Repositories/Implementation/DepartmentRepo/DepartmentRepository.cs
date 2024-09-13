using Domain.Entities;
using Infrastructure.DbConetxt;
using Infrastructure.Repositories.Interfaces.IDepartmentRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentModel>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Include(d => d.Director)
                .Include(d => d.Employees)
                .ThenInclude(e => e.RoleId)
                .ToListAsync();
        }

        public async Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Departments
            .Include(d => d.Director)
            .Include(d => d.Employees)
            .ThenInclude(e => e.RoleId)  // Include roles of employees
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task AddDepartmentAsync(DepartmentModel department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
