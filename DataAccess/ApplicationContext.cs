using DataAccess.DAL;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Preference> Preferences { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingStatus> MeetingStatuses { get; set; }
    public DbSet<MeetingType> MeetingTypes { get; set; }
    public DbSet<Place> Places { get; set; }

}

