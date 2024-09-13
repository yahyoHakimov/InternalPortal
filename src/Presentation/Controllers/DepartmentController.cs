using Domain.Entities;
using Infrastructure.Repositories.Interfaces.IDepartmentRepo;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentModel department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _departmentRepository.AddDepartmentAsync(department);
            await _departmentRepository.AddDepartmentAsync(department);
            return Ok(department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentModel department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest("Department ID mismatch");
            }

            var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            await _departmentRepository.UpdateDepartmentAsync(department);
            return Ok(department);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            await _departmentRepository.DeleteDepartmentAsync(id);
            return Ok();
        }
    }

}
