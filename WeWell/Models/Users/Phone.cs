using System.ComponentModel.DataAnnotations;

namespace WeWell.Models.Users;

public class Phone
{
    [Required]
    public string? PhoneNumber { get; set; }
}