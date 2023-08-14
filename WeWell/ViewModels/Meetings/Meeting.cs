namespace WeWell.ViewModels.Meetings;

public class MeetingCreate
{
    public int? CreatorId { get; set; }
    public int? GuestId { get; set; }
    public DateTime Date { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
    public int? TypeId { get; set; }
}
