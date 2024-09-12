using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface.IGoogle
{
    public interface IGoogleCalendarService
    {
        /// <summary>
        /// Gets the Google Calendar Service instance.
        /// </summary>
        /// <returns>CalendarService instance for interacting with Google Calendar API.</returns>
        CalendarService GetCalendarService();
        Event CreateTemporaryGoogleMeetEvent(CalendarService service, string calendarId);
        Task<string> GetCalendarIdAsync(CalendarService service);

        /// <summary>
        /// Creates a Google Meet event using the provided details.
        /// </summary>
        /// <param name="service">The CalendarService instance.</param>
        /// <param name="calendarId">The ID of the calendar where the event will be created.</param>
        /// <param name="summary">The summary or title of the event.</param>
        /// <param name="location">The location of the event.</param>
        /// <param name="description">The description of the event.</param>
        /// <param name="startTime">The start time of the event.</param>
        /// <param name="endTime">The end time of the event.</param>
        /// <returns>The created Google Calendar event.</returns>
        Task<Event> CreateGoogleMeetEvent(
            CalendarService service,
            string calendarId,
            string summary,
            string location,
            string description,
            DateTime startTime,
            DateTime endTime);
    }
}
