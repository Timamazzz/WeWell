namespace DataAccess.DAL;

public class Preference
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<User> Users { get; set; } = new();
    public List<Place> Places { get; set; } = new();
}
