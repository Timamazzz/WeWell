using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MeetingStatusRepository : IRepository<MeetingStatus>
{
    private readonly ApplicationContext _context;

    public MeetingStatusRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(MeetingStatus meetingStatus)
    {
        await _context.AddAsync(meetingStatus);
        await _context.SaveChangesAsync();

        return meetingStatus.Id;
    }

    public async Task<List<MeetingStatus>?> GetAllAsync()
    {
        List<MeetingStatus> meetingStatuses = await _context.MeetingStatuses.OrderBy(status => status.Id).ToListAsync();
        return meetingStatuses;
    }

    public async Task<MeetingStatus?> GetByIdAsync(int? id)
    {
        MeetingStatus? meetingStatus = await _context.MeetingStatuses.SingleOrDefaultAsync(status => status.Id == id);
        return meetingStatus;
    }

    public async Task UpdateAsync(MeetingStatus meetingStatus)
    {
        _context.Update(meetingStatus);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var meetingStatus = await _context.MeetingStatuses.FindAsync(id);
        if (meetingStatus != null)
        {
            _context.MeetingStatuses.Remove(meetingStatus);
            await _context.SaveChangesAsync();
        }
    }

}
