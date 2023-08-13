﻿namespace Domain.DTO;

public class Place
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? ImagePath { get; set; }
    public byte[]? Image { get; set; }
    public string? ImageExtensions { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public TimeSpan? StartWork { get; set; }
    public TimeSpan? EndWork { get; set; }
    public List<int>? PreferencesId { get; set; }
    public List<int>? MeetingTypesId { get; set; }
    public int? MinDurationHours { get; set; }
    public int? MaxDurationHours { get; set; }
}
