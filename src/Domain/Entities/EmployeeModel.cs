using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }

        public int ApplicationUserId { get; set; }  // Link to Identity User
        public ApplicationUser ApplicationUser { get; set; }  // Navigation property

        // Relationships
        public ICollection<KPI> KPIs { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
