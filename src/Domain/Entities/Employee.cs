using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }

        public string ApplicationUserId { get; set; }  // Link to Identity User
        public ApplicationUser ApplicationUser { get; set; }  // Navigation property
    }


}
