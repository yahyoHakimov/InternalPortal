using Application.Services.Interface.IGoogle;
using Domain.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation.Google
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IMeetingRepository _meetingRepository;
        private const string ApplicationName = "BankInternalPortal";
        private static readonly string[] Scopes = { CalendarService.Scope.Calendar };

        public GoogleCalendarService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        // Updated to use Service Account credentials instead of OAuth2 for individual user
        public CalendarService GetCalendarService()
        {
            var serviceAccountEmail = "googlemeet@bank-433605.iam.gserviceaccount.com";
            var jsonKeyFilePath = Path.Combine("D:\\Hackaton\\InternalPortal\\", "Secrets", "bank-433605-54bfb267cf18.json");

            try
            {
                var credential = GoogleCredential.FromFile(jsonKeyFilePath)
                    .CreateScoped(CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents);


                return new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create CalendarService: {ex.Message}");
                throw;
            }
        }

        public async Task<Event> CreateGoogleMeetEvent(CalendarService service, string calendarId, string summary, string location, string description, DateTime startTime, DateTime endTime)
        {
            var newEvent = new Event
            {
                Summary = summary,
                Location = location,
                Description = description,
                Start = new EventDateTime
                {
                    DateTime = startTime,
                    TimeZone = "America/Los_Angeles", // Adjust timezone as needed
                },
                End = new EventDateTime
                {
                    DateTime = endTime,
                    TimeZone = "America/Los_Angeles", // Adjust timezone as needed
                },
                ConferenceData = new ConferenceData
                {
                    CreateRequest = new CreateConferenceRequest
                    {
                        RequestId = Guid.NewGuid().ToString(),
                        ConferenceSolutionKey = new ConferenceSolutionKey { Type = "hangoutsMeet" }
                    }
                }
            };

            var request = service.Events.Insert(newEvent, calendarId);
            request.ConferenceDataVersion = 1;

            return await request.ExecuteAsync();
        }


        public Event CreateTemporaryGoogleMeetEvent(CalendarService service, string calendarId)
        {
            try
            {
                var newEvent = new Event
                {
                    Summary = "Temporary Event",
                    Start = new EventDateTime { DateTime = DateTime.UtcNow },
                    End = new EventDateTime { DateTime = DateTime.UtcNow.AddHours(1) }, // Temporary time
                    ConferenceData = new ConferenceData
                    {
                        CreateRequest = new CreateConferenceRequest
                        {
                            RequestId = Guid.NewGuid().ToString(),
                            ConferenceSolutionKey = new ConferenceSolutionKey { Type = "hangoutsMeet" }
                        }
                    }
                };

                var request = service.Events.Insert(newEvent, calendarId);
                request.ConferenceDataVersion = 1;

                var createdEvent = request.Execute();

                // Log the Meet link
                if (createdEvent.ConferenceData != null && createdEvent.ConferenceData.EntryPoints != null)
                {
                    foreach (var entryPoint in createdEvent.ConferenceData.EntryPoints)
                    {
                        Console.WriteLine($"Google Meet Link: {entryPoint.Uri}");
                    }
                }
                else
                {
                    Console.WriteLine("No ConferenceData returned from Google.");
                }

                return createdEvent;
            }
            catch (GoogleApiException ex)
            {
                // Log Google API-specific errors
                Console.WriteLine($"Google API error: {ex.Error.Code} - {ex.Error.Message}");
                foreach (var error in ex.Error.Errors)
                {
                    Console.WriteLine($"Error Reason: {error.Reason}, Message: {error.Message}");
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating temporary Google Meet event: " + ex.Message);
                throw;
            }
        }


        public async Task<string> GetCalendarIdAsync(CalendarService service)
        {
            try
            {
                // Get the list of available calendars
                var calendarListRequest = service.CalendarList.List();
                var calendarList = await calendarListRequest.ExecuteAsync();

                // Log available calendars for debugging
                if (calendarList.Items != null && calendarList.Items.Any())
                {
                    foreach (var calendar in calendarList.Items)
                    {
                        Console.WriteLine($"Found Calendar: {calendar.Summary} (ID: {calendar.Id})");
                    }
                }

                // Attempt to find the primary calendar
                var primaryCalendar = calendarList.Items.FirstOrDefault(c => c.Primary == true);
                if (primaryCalendar != null)
                {
                    return primaryCalendar.Id; // Return the primary calendar ID
                }

                // If no primary calendar, try to select the first available calendar
                if (calendarList.Items.Any())
                {
                    var defaultCalendar = calendarList.Items.First();
                    Console.WriteLine($"Using the first available calendar: {defaultCalendar.Summary} (ID: {defaultCalendar.Id})");
                    return defaultCalendar.Id;
                }

                // If no calendars are available, log the issue
                Console.WriteLine("No calendars found for the user.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving calendar ID: {ex.Message}");
                throw;
            }
        }

    }
}
