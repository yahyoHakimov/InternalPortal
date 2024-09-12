using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected

        // Foreign Key
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
