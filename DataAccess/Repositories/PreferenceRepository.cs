using AutoMapper;
using DataAccess.Models;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PreferenceRepository/* : IRepository<Preference>*/
{
    private readonly ApplicationContext _context;

    public PreferenceRepository(ApplicationContext context)
    {
        _context = context;
    }

    /*public async Task<int?> CreateAsync(Preference preference)
    {
        await _context.AddAsync(preference);
        await _context.SaveChangesAsync();

        return preference.Id;
    }

    public async Task<List<Preference>?> GetAllAsync()
    {
        List<Preference> preferences = await _context.Preferences.OrderBy(p => p.Id).ToListAsync();
        return preferences;
    }

    public async Task<List<Preference>?> GetPreferencesByIdsAsync(List<int> preferenceIds)
    {
        List<Preference>? preferences = await _context.Preferences
            .Where(p => preferenceIds.Contains(p.Id))
            .ToListAsync();

        return preferences;
    }

    public async Task<Preference?> GetByIdAsync(int? id)
    {
        Preference? preference = await _context.Preferences.SingleOrDefaultAsync(p => p.Id == id);
        return preference;
    }

    public async Task UpdateAsync(Preference preference)
    {
        _context.Update(preference);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Preference? preference = await _context.Preferences.FindAsync(id);

        if (preference != null)
        {
            _context.Remove(preference);
            await _context.SaveChangesAsync();
        }
    }*/
}

