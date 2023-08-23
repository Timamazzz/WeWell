namespace WeWell.Models.Meetings;

public class MeetingUpdate
{
    public int Id { get; set; }
    public string? Status { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsShowForCreator { get; set; }
    public bool? IsShowForGuest { get; set; }
}
