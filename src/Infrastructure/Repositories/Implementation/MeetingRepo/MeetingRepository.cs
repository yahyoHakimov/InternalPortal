using Domain.Entities;
using Infrastructure.DbConetxt;
using Infrastructure.Repositories.Interfaces.IMeetingRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation.MeetingRepo
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly ApplicationDbContext _context;  // Assuming you're using Entity Framework

        public MeetingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all meetings
        public async Task<List<Meeting>> GetAllAsync()
        {
            return await _context.Meetings.ToListAsync();
        }

        // Add a new meeting
        public async Task AddMeetAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        // Update an existing meeting
        public async Task UpdateMeetAsync(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
            await _context.SaveChangesAsync();
        }

        // Find a meeting by its ID
        public async Task<Meeting> GetByIdAsync(Guid meetingId)
        {
            return await _context.Meetings.FindAsync(meetingId);
        }

        // Delete a meeting by its ID
        public async Task DeleteMeetAsync(Guid meetingId)
        {
            var meeting = await GetByIdAsync(meetingId);
            if (meeting != null)
            {
                _context.Meetings.Remove(meeting);
                await _context.SaveChangesAsync();
            }
        }
    }
}