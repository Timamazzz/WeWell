using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MeetingRepository : IRepository<DTO.Meeting>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public MeetingRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(DTO.Meeting meetingData)
    {
        DAL.Meeting meeting = _mapper.Map<DAL.Meeting>(meetingData);

        await _context.AddAsync(meeting);
        await _context.SaveChangesAsync();

        return meeting.Id;
    }

    public async Task<List<DTO.Meeting>?> GetAllAsync()
    {
        List<DAL.Meeting> meetings = await _context.Meetings.OrderBy(meeting => meeting.Id).ToListAsync();
        List<DTO.Meeting> result = _mapper.Map<List<DTO.Meeting>>(meetings);

        return result;
    }

    public async Task<DTO.Meeting?> GetByIdAsync(int? id)
    {
        DAL.Meeting? meeting = await _context.Meetings.SingleOrDefaultAsync(m => m.Id == id);
        DTO.Meeting? result = _mapper.Map<DTO.Meeting>(meeting);

        return result;
    }

    public async Task UpdateAsync(DTO.Meeting meetingData)
    {
        DAL.Meeting? meeting = await _context.Meetings.FindAsync(meetingData.Id);

        if (meeting != null)
        {
            _mapper.Map(meetingData, meeting);
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(int id)
    {
        DAL.Meeting? meeting = await _context.Meetings.FindAsync(id);

        if (meeting != null)
        {
            _context.Remove(meeting);
            await _context.SaveChangesAsync();
        }
    }
}
