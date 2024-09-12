using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Meeting
{
    public class MeetingDTO
    {
        public int Duration { get; set; }

        public string HostName { get; set; }

        public string MeetingTitle { get; set; }

        public DateTime MeetingDateTime { get; set; }

        public string MeetUrl { get; set; }

        public Guid? CreatedBy { get; set; } // Employee who scheduled the meeting

        public Guid? UpdatedBy { get; set; } // Employee who last modified the meeting
    }
}
