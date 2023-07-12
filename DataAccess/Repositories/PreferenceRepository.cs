using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PreferenceRepository : IRepository<Domain.DTO.Preference, DAL.Preference>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public PreferenceRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Domain.DTO.Preference preferenceData)
    {
        DAL.Preference preference = _mapper.Map<DAL.Preference>(preferenceData);

        await _context.AddAsync(preference);
        await _context.SaveChangesAsync();

        return preference.Id;
    }

    public async Task<List<Domain.DTO.Preference>> GetAllAsync()
    {
        List<DAL.Preference> preferences = await _context.Preferences.OrderBy(preference => preference.Id).ToListAsync();
        List<Domain.DTO.Preference> result = _mapper.Map<List<Domain.DTO.Preference>>(preferences);

        return result;
    }

    public async Task<Domain.DTO.Preference?> GetByIdAsync(int? id)
    {
        DAL.Preference? preference = await _context.Preferences.SingleOrDefaultAsync(p => p.Id == id);
        Domain.DTO.Preference? result = _mapper.Map<Domain.DTO.Preference>(preference);

        return result;
    }

    public async Task UpdateAsync(Domain.DTO.Preference preferenceData)
    {
        DAL.Preference? preference = await _context.Preferences.FindAsync(preferenceData.Id);

        if (preference != null)
        {
            _mapper.Map(preferenceData, preference);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        DAL.Preference? preference = await _context.Preferences.FindAsync(id);

        if (preference != null)
        {
            _context.Remove(preference);
            await _context.SaveChangesAsync();
        }
    }
}
