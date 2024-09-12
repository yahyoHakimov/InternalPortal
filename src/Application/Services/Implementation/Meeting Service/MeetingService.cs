using Application.DTOs.Meeting;
using Application.Services.Interface.IMeeting;
using Domain.Entities;
using Google.Apis.Calendar.v3;
using Infrastructure.Repositories.Interfaces.IMeetingRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation.MeetingService
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;
        

        private readonly string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents };
        public MeetingService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<List<MeetingDTO>> GetAllMeetingsAsync()
        {
            var meetings = await _meetingRepository.GetAllAsync();
            return meetings.Select(meeting => new MeetingDTO
            {
                Duration = meeting.Duration,
                HostName = meeting.HostName,
                MeetingTitle = meeting.MeetingTitle,
                MeetingDateTime = meeting.MeetingDateTime,
                MeetUrl = meeting.MeetUrl
            }).ToList();
        }

        public async Task<MeetingDTO> SaveMeetingAsync(MeetingDTO meetingDto)
        {
            // Map the DTO to the entity
            var meeting = new Meeting
            {
                Duration = meetingDto.Duration,
                HostName = meetingDto.HostName,
                MeetingTitle = meetingDto.MeetingTitle,
                MeetingDateTime = meetingDto.MeetingDateTime,
                MeetUrl = meetingDto.MeetUrl,
            };

            // Save the meeting in the repository
            await _meetingRepository.AddMeetAsync(meeting);

            // Map the entity back to the DTO and return it
            return new MeetingDTO
            {
                Duration = meetingDto.Duration,
                HostName = meetingDto.HostName,
                MeetingTitle = meetingDto.MeetingTitle,
                MeetingDateTime = meetingDto.MeetingDateTime,
                MeetUrl = meetingDto.MeetUrl
            };
        }
    }
}
