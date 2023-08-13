namespace DataAccess.DAL;

public class Place
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? ImagePath { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public TimeSpan? StartWork { get; set; }
    public TimeSpan? EndWork { get; set; }
    public List<Meeting> Meetings { get; set; } = new List<Meeting>();
    public List<Preference> Preferences { get; set; } = new List<Preference>();
    public List<MeetingType> MeetingTypes { get; set; } = new List<MeetingType>();
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
}
