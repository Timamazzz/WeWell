using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MeetingRepository /*: IRepository<Meeting>*/
{
    private readonly ApplicationContext _context;

    public MeetingRepository(ApplicationContext context)
    {
        _context = context;
    }

    /*public async Task<int?> CreateAsync(Meeting meeting)
    {

        await _context.AddAsync(meeting);
        await _context.SaveChangesAsync();

        return meeting.Id;
    }

    public async Task<List<Meeting>?> GetAllAsync()
    {
        List<Meeting> meetings = await _context.Meetings.OrderBy(meeting => meeting.Id).ToListAsync();

        return meetings;
    }

    public async Task<Meeting?> GetByIdAsync(int? id)
    {
        Meeting? meeting = await _context.Meetings.SingleOrDefaultAsync(m => m.Id == id);
        return meeting;
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
    }*/
}
