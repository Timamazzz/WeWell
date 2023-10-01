namespace Domain.DataTransferObjects;

public class Player
{
    public int? Id { get; set; }
    public string? Username { get; set; }
    public TimeSpan? Time { get; set; }
}