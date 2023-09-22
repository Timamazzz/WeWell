using System.ComponentModel.DataAnnotations;

namespace WeWell.Models.Users;

public class UserCreate
{
    public string? Name { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public bool? IsAllPreferences { get; set; }
    public List<Preference>? Preferences { get; set; }
}
