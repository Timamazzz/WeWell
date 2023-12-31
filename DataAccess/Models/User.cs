﻿using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class User
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? AvatarPath { get; set; }
    [Required]
    public bool? IsAllPreferences { get; set; }
    public List<Preference> Preferences { get; set; } = new List<Preference>();
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
}
