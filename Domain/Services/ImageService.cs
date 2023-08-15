namespace Domain.Services;

public class ImageService
{
    public string? _webRootPath;
    public ImageService()
    {
    }

    private async Task<string> SaveImage(string? extensions, byte[] image, string pathToUpload)
    {
        if (extensions == null)
        {
            throw new ArgumentNullException(nameof(extensions), "Extensions cannot be null.");
        }

        string fullPath = Path.Combine(_webRootPath, pathToUpload);

        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);

        var fileName = $"{Guid.NewGuid()}{extensions}";
        fullPath = Path.Combine(fullPath, fileName);

        await using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await fileStream.WriteAsync(image);
        }

        return Path.Combine(pathToUpload, fileName);
    }

    private void DeleteImage(string? filePath)
    {
        if (filePath != null)
        {
            string fullPath = Path.Combine(_webRootPath, filePath);

            if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    private async Task<string> ReplaceImage(string imagePath, byte[] image, string pathToUpload)
    {
        DeleteImage(imagePath);

        var extensions = Path.GetExtension(imagePath);
        if (extensions == null)
        {
            throw new ArgumentNullException(nameof(extensions), "Extensions cannot be null.");
        }

        
        return await SaveImage(extensions, image, pathToUpload);
    }
}
