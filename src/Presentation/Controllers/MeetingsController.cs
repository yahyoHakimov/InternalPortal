using Application.DTOs.Meeting;
using Application.Services.Interface.IMeeting;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class MeetingsController : ControllerBase
    {
        private readonly string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents };
        private readonly IMeetingService _meetingService;

        public MeetingsController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpPost("meetings/create-for-later")]
        public async Task<IActionResult> CreateMeetingForLater([FromBody] GoogleMeetingRequest request)
        {
            try
            {
                var credential = GetGoogleCredential(request.AccessToken);
                var calendarService = GetCalendarService(credential);

                Event newEvent = new Event()
                {
                    Summary = "Google Meet for later",
                    Start = new EventDateTime()
                    {
                        DateTime = DateTime.Now.AddHours(1),
                        TimeZone = "UTC",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = DateTime.Now.AddHours(2),
                        TimeZone = "UTC",
                    },
                    ConferenceData = new ConferenceData()
                    {
                        CreateRequest = new CreateConferenceRequest()
                        {
                            RequestId = Guid.NewGuid().ToString(),
                            ConferenceSolutionKey = new ConferenceSolutionKey()
                            {
                                Type = "hangoutsMeet"
                            }
                        }
                    }
                };

                var createdEvent = await calendarService.Events.Insert(newEvent, "primary")
                    .ExecuteAsync();

                return Ok(new { meetingUrl = createdEvent.HangoutLink });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("meetings/schedule")]
        public async Task<IActionResult> ScheduleMeeting([FromBody] GoogleMeetingRequest request)
        {
            try
            {
                // Create Google Credential with required scopes
                var credential = GoogleCredential.FromAccessToken(request.AccessToken)
                    .CreateScoped(CalendarService.Scope.Calendar);

                // Initialize the Calendar service
                var calendarService = new CalendarService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Google Meet Integration",
                });

                // Create the event
                Event newEvent = new Event()
                {
                    Summary = "Scheduled Google Meet",
                    Start = new EventDateTime()
                    {
                        DateTime = DateTime.Parse(request.StartTime),
                        TimeZone = "UTC", // Change if necessary
                    },
                    End = new EventDateTime()
                    {
                        DateTime = DateTime.Parse(request.EndTime),
                        TimeZone = "UTC", // Change if necessary
                    },
                    ConferenceData = new ConferenceData()
                    {
                        CreateRequest = new CreateConferenceRequest()
                        {
                            RequestId = Guid.NewGuid().ToString(),
                            ConferenceSolutionKey = new ConferenceSolutionKey()
                            {
                                Type = "hangoutsMeet"
                            }
                        }
                    }
                };

                // Insert event into calendar
                var requestSettings = calendarService.Events.Insert(newEvent, "primary");
                requestSettings.ConferenceDataVersion = 1; // Ensure ConferenceData is included

                // Execute the event creation request
                var createdEvent = await requestSettings.ExecuteAsync();

                return Ok(new { meetingUrl = createdEvent.HangoutLink });
            }
            catch (GoogleApiException ex)
            {
                // Detailed error handling
                return StatusCode((int)ex.HttpStatusCode, new { error = ex.Message, details = ex.Error });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }


        private GoogleCredential GetGoogleCredential(string accessToken)
        {
            return GoogleCredential.FromAccessToken(accessToken).CreateScoped(Scopes);
        }

        private CalendarService GetCalendarService(GoogleCredential credential)
        {
            return new CalendarService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Meet Integration",
            });
        }

        [HttpPost("meetings/create")]
        public async Task<IActionResult> SaveMeeting([FromBody] MeetingDTO meetingDto)
        {
            if (meetingDto == null)
            {
                return BadRequest("Meeting details are required.");
            }

            try
            {
                var createdMeeting = await _meetingService.SaveMeetingAsync(meetingDto);

                if (createdMeeting == null)
                {
                    return StatusCode(500, "An error occurred while creating the meeting.");
                }

                return Ok(new
                {
                    message = "Meeting created successfully.",
                    createdMeeting
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("meetings/all")]
        public async Task<IActionResult> GetAllMeetings()
        {
            try
            {
                var meetings = await _meetingService.GetAllMeetingsAsync();
                return Ok(meetings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }

    public class GoogleMeetingRequest
    {
        public string AccessToken { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
