using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PlaceRepository : IRepository<Domain.DTO.Place, DAL.Place>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public PlaceRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Domain.DTO.Place placeData)
    {
        DAL.Place place = _mapper.Map<DAL.Place>(placeData);

        await _context.AddAsync(place);
        await _context.SaveChangesAsync();

        return place.Id;
    }

    public async Task<List<Domain.DTO.Place>> GetAllAsync()
    {
        List<DAL.Place> places = await _context.Places.OrderBy(place => place.Id).ToListAsync();
        List<Domain.DTO.Place> result = _mapper.Map<List<Domain.DTO.Place>>(places);

        return result;
    }

    public async Task<Domain.DTO.Place?> GetByIdAsync(int? id)
    {
        DAL.Place? place = await _context.Places.SingleOrDefaultAsync(p => p.Id == id);
        Domain.DTO.Place? result = _mapper.Map<Domain.DTO.Place>(place);

        return result;
    }

    public async Task UpdateAsync(Domain.DTO.Place placeData)
    {
        DAL.Place? place = await _context.Places.FindAsync(placeData.Id);

        if (place != null)
        {
            _mapper.Map(placeData, place);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        DAL.Place? place = await _context.Places.FindAsync(id);

        if (place != null)
        {
            _context.Remove(place);
            await _context.SaveChangesAsync();
        }
    }
}
