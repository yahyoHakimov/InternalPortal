using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        public int Duration { get; set; }

        public string HostName { get; set; }
        public string MeetingTitle { get; set; }
        public DateTime MeetingDateTime { get; set; }
        public string MeetUrl { get; set; }

        // Foreign key to Employee who created the meeting
        public int CreatedByEmployeeId { get; set; }
        [ForeignKey("CreatedByEmployeeId")]
        public EmployeeModel CreatedBy { get; set; }

        // Foreign key to Employee who updated the meeting
        public int? UpdatedByEmployeeId { get; set; }
        [ForeignKey("UpdatedByEmployeeId")]
        public EmployeeModel UpdatedBy { get; set; }
    }
}
