namespace DataAccess.DTO;

public class Place
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Adres { get; set; }
    public string? ImagePath { get; set; }
    public string? Price { get; set; }
    public TimeSpan? StartWork { get; set; }
    public TimeSpan? EndWork { get; set; }

}
