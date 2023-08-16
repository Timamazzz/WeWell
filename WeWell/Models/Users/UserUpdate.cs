using System.ComponentModel.DataAnnotations;

namespace WeWell.Models.Users;

public class UserUpdate
{
    [Required]
    public int? Id { get; set; }
    public string? Name { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public bool? IsAllPreferences { get; set; }
    public List<Preference>? Preferences { get; set; }
}
