using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MeetingStatusRepository : IRepository<DTO.MeetingStatus>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public MeetingStatusRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(DTO.MeetingStatus meetingStatusData)
    {
        DAL.MeetingStatus meetingStatus = _mapper.Map<DAL.MeetingStatus>(meetingStatusData);

        await _context.AddAsync(meetingStatus);
        await _context.SaveChangesAsync();

        return meetingStatus.Id;
    }

    public async Task<List<DTO.MeetingStatus>?> GetAllAsync()
    {
        List<DAL.MeetingStatus> meetingStatuses = await _context.MeetingStatuses.OrderBy(status => status.Id).ToListAsync();
        List<DTO.MeetingStatus> result = _mapper.Map<List<DTO.MeetingStatus>>(meetingStatuses);

        return result;
    }

    public async Task<DTO.MeetingStatus?> GetByIdAsync(int? id)
    {
        DAL.MeetingStatus? meetingStatus = await _context.MeetingStatuses.SingleOrDefaultAsync(status => status.Id == id);
        DTO.MeetingStatus? result = _mapper.Map<DTO.MeetingStatus>(meetingStatus);

        return result;
    }

    public async Task UpdateAsync(DTO.MeetingStatus meetingStatusData)
    {
        DAL.MeetingStatus? meetingStatus = await _context.MeetingStatuses.FindAsync(meetingStatusData.Id);

        if (meetingStatus != null)
        {
            _mapper.Map(meetingStatusData, meetingStatus);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        DAL.MeetingStatus? meetingStatus = await _context.MeetingStatuses.FindAsync(id);

        if (meetingStatus != null)
        {
            _context.Remove(meetingStatus);
            await _context.SaveChangesAsync();
        }
    }
}
