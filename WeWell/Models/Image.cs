namespace WeWell.Models;

public class Image
{
    public int ParentModelId { get; set; }
    public IFormFile? ImageFile { get; set;  }
}