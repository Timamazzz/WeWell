using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Preference
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public List<User>? Users { get; set; }
    public List<Place>? Places { get; set; }
}
