using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public string Title { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string CreatedBy { get; set; }
        public string Participants { get; set; } // List of employee IDs or names
        public string MeetingUrl { get; set; } // Google Meet URL or other
    }

}
