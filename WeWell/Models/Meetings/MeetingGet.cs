using WeWell.Models.Places;
using WeWell.Models.Users;

namespace WeWell.Models.Meetings;

public class MeetingGet
{
    public int Id { get; set; }
    public UserGet? Creator { get; set; }
    public UserGet? Guest { get; set; }
    public DateTime Date { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
    public MeetingType? Type { get; set; }
    public string? Status { get; set; }
    public PlaceGet? Place { get; set; }
    public bool? IsArchive { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsShowForCreator { get; set; }
    public bool? IsShowForGuest { get; set; }
}