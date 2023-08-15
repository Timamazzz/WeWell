namespace Domain.DataTransferObjects;

public class Meeting
{
    public int Id { get; set; }
    public User? Creator { get; set; }
    public User? GuestId { get; set; }
    public DateTime Date { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
    public MeetingType? Type { get; set; }
    public MeetingStatus? Status { get; set; }
    public Place? Place { get; set; }
}
