using Application.DTOs.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface.IMeeting
{
    public interface IMeetingService
    {
        /*Task<ExternalMeetingDto> ScheduleMeeting(ExternalMeetingDto meetingDto);*/
        Task<List<MeetingDTO>> GetAllMeetingsAsync();
        Task<MeetingDTO> SaveMeetingAsync(MeetingDTO meetingDto);
    }
}
