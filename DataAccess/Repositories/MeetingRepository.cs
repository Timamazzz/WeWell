using DataAccess.Enums;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MeetingRepository : IRepository<Meeting>
{
    private readonly ApplicationContext _context;

    public MeetingRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(Meeting meeting)
    {

        await _context.AddAsync(meeting);
        await _context.SaveChangesAsync();

        return meeting.Id;
    }

    public async Task<List<Meeting>?> GetAllAsync()
    {
        List<Meeting> meetings = await _context.Meetings.
            Include(m => m.Creator).
            Include(m => m.Guest).
            Include(m => m.Place).
            OrderBy(meeting => meeting.Id).ToListAsync();

        return meetings;
    }

    public async Task<Meeting?> GetByIdAsync(int? id)
    {
        Meeting? meeting = await _context.Meetings.SingleOrDefaultAsync(m => m.Id == id);
        return meeting;
    }

    public async Task<List<Meeting>?> GetMeetingsByUserIdAsync(int userId)
    {
        List<Meeting> meetings = await _context.Meetings
            .Where(meeting => (meeting.Creator.Id == userId || meeting.Guest.Id == userId) && meeting.IsActive == true)
            .Include(m => m.Creator)
            .Include(m => m.Guest)
            .Include(m => m.Place)
            .Include(m => m.Type)
            .Where(meeting =>
                (meeting.Status != MeetingStatus.Cancelled.ToString() || 
                 (meeting.Status == MeetingStatus.Cancelled.ToString() && 
                  (meeting.IsShowForCreator.Value || meeting.Creator.Id != userId) &&
                  (meeting.IsShowForGuest.Value || meeting.Guest.Id != userId)))
            )
            .OrderBy(meeting => meeting.Id)
            .ToListAsync();

        return meetings;
    }
    
    public async Task UpdateAsync(Meeting meeting)
    {
        _context.Update(meeting);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        Meeting? meeting = await _context.Meetings.FindAsync(id);

        if (meeting != null)
        {
            _context.Remove(meeting);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task DeleteAllAsync()
    {
        var allMeetings = await _context.Meetings.ToListAsync();

        if (allMeetings != null && allMeetings.Any())
        {
            _context.RemoveRange(allMeetings);
            await _context.SaveChangesAsync();
        }
    }

}
