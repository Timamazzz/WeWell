namespace Domain.DataTransferObjects;

public class Meeting
{
    public int Id { get; set; }
    public User? Creator { get; set; }
    public int? CreatorId { get; set; }
    public User? Guest { get; set; }
    public int? GuestId { get; set; }
    public DateTime Date { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
    public MeetingType? Type { get; set; }
    public string? Status { get; set; }
    public Place? Place { get; set; }
    public bool? IsArchive { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsShowForCreator { get; set; }
    public bool? IsShowForGuest { get; set; }
}
