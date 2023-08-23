namespace WeWell.Models.Places;

public class PlaceCreate
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
    public List<Preference>? Preferences { get; set; }
    public List<MeetingType>? MeetingTypes { get; set; }
}
