namespace Domain.Services;

public class ImageService
{
    public ImageService()
    {
    }

    public async Task<string> SaveImage(string? extensions, byte[] image, string _pathToUpload)
    {
        if (extensions == null)
        {
            throw new ArgumentNullException(nameof(extensions), "Extensions cannot be null.");
        }

        var fileName = $"{Guid.NewGuid()}{extensions}";
        var filePath = Path.Combine(_pathToUpload, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.WriteAsync(image);
        }

        return filePath;
    }

    public static void DeleteImage(string? filePath)
    {
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public async Task<string> ReplaceImage(string imagePath, byte[] image, string _pathToUpload)
    {
        DeleteImage(imagePath);

        var extensions = Path.GetExtension(imagePath);
        if (extensions == null)
        {
            throw new ArgumentNullException(nameof(extensions), "Extensions cannot be null.");
        }

        var newImagePath = await SaveImage(extensions, image, _pathToUpload);

        return newImagePath;
    }
}
