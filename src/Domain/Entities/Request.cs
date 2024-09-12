using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public string RequestType { get; set; } // Leave, WFH, etc.
        public string Status { get; set; } // Pending, Approved, Rejected
        public DateTime RequestedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
