namespace Domain.DTO;

public class Meeting
{
    public int Id { get; set; }
    public int? CreatorId { get; set; }
    public int? GuestId { get; set; }
    public DateTime Date { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
    public int? TypeId { get; set; }
    public int? StatusId { get; set; }
    public int? PlaceId { get; set; }
}
