using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Place
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Address { get; set; }
    public string? ImagePath { get; set; }
    [Required]
    public int? MinPrice { get; set; }
    [Required]
    public int? MaxPrice { get; set; }
    [Required]
    public int? MinDurationHours { get; set; }
    [Required]
    public int? MaxDurationHours { get; set; }
    public List<Meeting>? Meetings { get; set; }
    public List<Preference>? Preferences { get; set; }
    public List<MeetingType>? MeetingTypes { get; set; }
}
