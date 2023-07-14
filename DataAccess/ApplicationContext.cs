using DataAccess.DAL;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
/*        Database.EnsureDeleted();   // удаляем бд со старой схемой
        Database.EnsureCreated();   // создаем бд с новой схемой*/
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Preference> Preferences { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingStatus> MeetingStatuses { get; set; }
    public DbSet<MeetingType> MeetingTypes { get; set; }
    public DbSet<Place> Places { get; set; }

}

