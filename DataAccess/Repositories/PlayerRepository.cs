using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PlayerRepository : IRepository<Player>
{
    private readonly ApplicationContext _context;

    public PlayerRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(Player player)
    {
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();

        return player.Id;
    }

    public async Task<List<Player>> GetAllAsync()
    {
        var players = _context.Players.OrderBy(p => p.Time).ToList();
        return players;
    }

    public async Task<Player> GetByIdAsync(int? id)
    {
        Player player = await _context.Players.SingleOrDefaultAsync(p => p.Id == id);
        return player;
    }

    public async Task UpdateAsync(Player player)
    {
        _context.Update(player);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Player player = await _context.Players.FindAsync(id);

        if (player != null)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
    }
}