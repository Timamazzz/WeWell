namespace DataAccess.DTO;

public class Meeting
{
    public int Id { get; set; }
    public User? Creator { get; set; }
    public User? Guest { get; set; }
    public DateTime Date { get; set; }
    public int? Price { get; set; }
    public int? Duration { get; set; }
    public MeetingType? Type { get; set; }
    public MeetingStatus? Status { get; set; }
    public Place? Place { get; set; }
}
