using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Request
    {
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public string RequestType { get; set; } // Leave, WFH, etc.
        public string Status { get; set; } // Pending, Approved, Rejected
        public DateTime RequestedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }

}
