using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MeetingTypeRepository : IRepository<Domain.DTO.MeetingType, DAL.MeetingType>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public MeetingTypeRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Domain.DTO.MeetingType meetingTypeData)
    {
        DAL.MeetingType meetingType = _mapper.Map<DAL.MeetingType>(meetingTypeData);

        await _context.AddAsync(meetingType);
        await _context.SaveChangesAsync();

        return meetingType.Id;
    }

    public async Task<List<Domain.DTO.MeetingType>> GetAllAsync()
    {
        List<DAL.MeetingType> meetingTypes = await _context.MeetingTypes.OrderBy(type => type.Id).ToListAsync();
        List<Domain.DTO.MeetingType> result = _mapper.Map<List<Domain.DTO.MeetingType>>(meetingTypes);

        return result;
    }

    public async Task<Domain.DTO.MeetingType?> GetByIdAsync(int? id)
    {
        DAL.MeetingType? meetingType = await _context.MeetingTypes.SingleOrDefaultAsync(type => type.Id == id);
        Domain.DTO.MeetingType? result = _mapper.Map<Domain.DTO.MeetingType>(meetingType);

        return result;
    }

    public async Task UpdateAsync(Domain.DTO.MeetingType meetingTypeData)
    {
        DAL.MeetingType? meetingType = await _context.MeetingTypes.FindAsync(meetingTypeData.Id);

        if (meetingType != null)
        {
            _mapper.Map(meetingTypeData, meetingType);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        DAL.MeetingType? meetingType = await _context.MeetingTypes.FindAsync(id);

        if (meetingType != null)
        {
            _context.Remove(meetingType);
            await _context.SaveChangesAsync();
        }
    }
}
