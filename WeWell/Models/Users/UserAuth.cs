namespace WeWell.Models.Users;
using System.ComponentModel.DataAnnotations;


public class UserAuth
{
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Password { get; set; }
}