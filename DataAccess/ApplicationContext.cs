using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        //Database.EnsureDeleted();   // удаляем бд со старой схемой
        //Database.EnsureCreated();   // создаем бд с новой схемой
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Preference> Preferences { get; set; } = null!;
    public DbSet<Meeting> Meetings { get; set; } = null!;
    public DbSet<MeetingType> MeetingTypes { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
}


