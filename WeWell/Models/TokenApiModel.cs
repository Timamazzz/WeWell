using System.ComponentModel.DataAnnotations;

namespace WeWell.Models;

public class TokenApiModel
{
    [Required]
    public string? AccessToken { get; set; }
    [Required]
    public string? RefreshToken { get; set; }

}