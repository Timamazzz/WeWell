using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Meeting
{
    [Required]
    public int Id { get; set; }
    [Required]
    public User? Creator { get; set; }
    [Required]
    public User? Guest { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int? MinPrice { get; set; }
    [Required]
    public int? MaxPrice { get; set; }
    [Required]
    public int? MinDurationHours { get; set; }
    [Required]
    public int? MaxDurationHours { get; set; }
    [Required]
    public MeetingType? Type { get; set; }
    [Required]
    public MeetingStatus? Status { get; set; }
    [Required]
    public Place? Place { get; set; }
}
