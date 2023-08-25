using System.ComponentModel.DataAnnotations;

namespace WeWell.Models.Places;

public class PlacesExcel
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    public int? MinPrice { get; set; }
    [Required]
    public int? MaxPrice { get; set; }
    [Required]
    public int? MinDurationHours { get; set; }
    [Required]
    public int? MaxDurationHours { get; set; }
    public List<int>? PreferencesId { get; set; }
    public List<int>? MeetingTypesId { get; set; }
}