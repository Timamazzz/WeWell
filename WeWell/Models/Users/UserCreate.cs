using System.ComponentModel.DataAnnotations;

namespace WeWell.Models.Users;

public class UserCreate
{
    [Required]
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile? Avatar { get; set; }
    public bool? isAllPreferences { get; set; }
    public List<int>? PreferencesId { get; set; }
}