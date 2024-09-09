using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int EmployeeId { get; set; }
        public string FeedbackText { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Status { get; set; } // Pending, Resolved, InProgress
        public string ITSupportResponse { get; set; }
    }

}
