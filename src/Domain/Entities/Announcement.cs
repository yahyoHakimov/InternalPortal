using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Announcement
    {
        [Key]
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }

        // Foreign key to Employee or User who posted the announcement
        public int PostedByEmployeeId { get; set; }
        [ForeignKey("PostedByEmployeeId")]
        public EmployeeModel PostedBy { get; set; }

        public string TargetAudience { get; set; } // Target roles or departments
    }
}
