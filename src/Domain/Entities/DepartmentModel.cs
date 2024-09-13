using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DepartmentModel
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        // Navigation properties
        public EmployeeModel Director { get; set; }
        public List<EmployeeModel> Employees { get; set; }
    }
}
