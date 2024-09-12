using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string FaceIdImage { get; set; } // Link to face image for verification

        // Foreign Key
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
