using DataAccess.Models;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PlaceRepository : IRepository<Place>
{
    private readonly ApplicationContext _context;

    public PlaceRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(Place place)
    {
        await _context.AddAsync(place);
        await _context.SaveChangesAsync();

        return place.Id;
    }

    public async Task<List<Place>?> GetAllAsync()
    {
        List<Place> places = await _context.Places
            .Include(p => p.Preferences)
            .Include(p => p.MeetingTypes)
            .ToListAsync();
        return places;
    }


    public async Task<Place?> GetByIdAsync(int? id)
    {
        Place? place = await _context.Places
            .Include(p => p.Preferences)
            .Include(p => p.MeetingTypes)
            .SingleOrDefaultAsync(p => p.Id == id);
        return place;
    }

    public async Task UpdateAsync(Place place)
    {
        _context.Update(place);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Place? place = await _context.Places.FindAsync(id);

        if (place != null)
        {
            _context.Remove(place);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<Place>? GetByForMeetingCreate(List<Preference> preferences, int maxPrice, int maxDuration, int meetingTypeId)
    {
        var places = await _context.Places
            .Where(place =>
                (place.MaxPrice <= maxPrice) &&
                (place.MaxDurationHours <= maxDuration) &&
                (place.Preferences.All(p => preferences.Contains(p))) &&
                (place.MeetingTypes.Any(mt => mt.Id == meetingTypeId)))
            .ToListAsync();
        
        if (places.Count == 0)
        {
            return null;
        }

        var random = new Random();
        var randomIndex = random.Next(0, places.Count);

        return places[randomIndex];
    }
}