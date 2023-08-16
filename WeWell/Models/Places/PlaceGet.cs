namespace WeWell.Models.Places;

public class PlaceGet
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? ImagePath { get; set; }
    public string? Url { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public string? StartWork { get; set; }
    public string? EndWork { get; set; }
    public List<int>? Preferences { get; set; }
    public List<int>? MeetingTypes { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
}
