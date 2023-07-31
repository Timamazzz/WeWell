namespace WeWell.ViewModels;

public class User
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile? Avatar { get; set; }
    public bool? isAllPreferences { get; set; }
    public List<Preference>? Preferences { get; set; }
}
