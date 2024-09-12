using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public string FeedbackText { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Status { get; set; } // Pending, Resolved, InProgress
        public string ITSupportResponse { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
