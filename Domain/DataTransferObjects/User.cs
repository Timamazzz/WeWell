namespace Domain.DataTransferObjects;
public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? AvatarPath { get; set; }
        public bool? IsAllPreferences { get; set; }
        public List<Preference>? Preferences { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
