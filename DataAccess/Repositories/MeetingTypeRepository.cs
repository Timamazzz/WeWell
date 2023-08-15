using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MeetingTypeRepository /*: IRepository<MeetingType>*/
{
    private readonly ApplicationContext _context;

    public MeetingTypeRepository(ApplicationContext context)
    {
        _context = context;
    }

    /*public async Task<int?> CreateAsync(MeetingType meetingType)
    {
        await _context.AddAsync(meetingType);
        await _context.SaveChangesAsync();

        return meetingType.Id;
    }

    public async Task<List<MeetingType>?> GetAllAsync()
    {
        List<MeetingType> meetingTypes = await _context.MeetingTypes.OrderBy(type => type.Id).ToListAsync();
        return meetingTypes;
    }

    public async Task<MeetingType?> GetByIdAsync(int? id)
    {
        MeetingType? meetingType = await _context.MeetingTypes.SingleOrDefaultAsync(type => type.Id == id);
        return meetingType;
    }

    public async Task UpdateAsync(MeetingType meetingType)
    {
        _context.Update(meetingType);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        MeetingType? meetingType = await _context.MeetingTypes.FindAsync(id);

        if (meetingType != null)
        {
            _context.Remove(meetingType);
            await _context.SaveChangesAsync();
        }
    }*/
}