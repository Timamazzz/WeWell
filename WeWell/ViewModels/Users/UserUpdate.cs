using System.ComponentModel.DataAnnotations;

namespace WeWell.ViewModels.Users;

public class UserUpdate
{
    [Required]
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile? Avatar { get; set; }
    public bool? isAllPreferences { get; set; }
    public List<int>? PreferencesId { get; set; }
}