namespace Domain.DTO;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AvatarPath { get; set; }
    public bool? isAllPreferences { get; set; }
    public List<int>? PreferencesId { get; set; }
}
