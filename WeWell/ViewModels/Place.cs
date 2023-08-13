namespace WeWell.ViewModels;

public class Place
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Adres { get; set; }
    public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; }
    public string? Price { get; set; }
    public string? StartWork { get; set; }
    public string? EndWork { get; set; }
    public List<int>? PreferencesId { get; set; }
    public List<int>? MeetingTypesId { get; set; }
}
