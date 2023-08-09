namespace Domain.DTO;

public class Meeting
{
    public int Id { get; set; }
    public int? CreatorId { get; set; }
    public int? GuestId { get; set; }
    public DateTime Date { get; set; }
    public int? Price { get; set; }
    public int? Duration { get; set; }
    public int? TypeId { get; set; }
    public int? StatusId { get; set; }
    public int? PlaceId { get; set; }
}
