using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces.IEmployeeRepo
{
    public interface IEmployeeRepository
    {
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(EmployeeModel employee);
        Task UpdateEmployeeAsync(EmployeeModel employee);
        Task DeleteEmployeeAsync(int employeeId);
    }

}
