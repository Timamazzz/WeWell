namespace DataAccess.DAL;

public class MeetingStatus
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Meeting> Meetings { get; set; } = new List<Meeting>();
}
