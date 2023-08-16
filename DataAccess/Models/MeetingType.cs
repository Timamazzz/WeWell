using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class MeetingType
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public List<Meeting>? Meetings { get; set; }
}
