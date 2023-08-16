namespace WeWell.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ImageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    private async Task<string> SaveImage(IFormFile? image, string pathToUpload)
    {
        if (image == null || image.Length == 0)
        {
            throw new ArgumentException("Invalid image file");
        }

        if (string.IsNullOrEmpty(pathToUpload))
        {
            throw new ArgumentNullException(nameof(pathToUpload), "Path to upload cannot be null or empty.");
        }

        var fileExtension = Path.GetExtension(image.FileName);
        if (string.IsNullOrEmpty(fileExtension) || !IsSupportedImageFormat(fileExtension))
        {
            throw new ArgumentException("Unsupported image format");
        }

        string fileName = $"{Guid.NewGuid()}{fileExtension}";
        string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, pathToUpload, fileName);
        
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        await using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        return Path.Combine(pathToUpload, fileName);
    }

    private bool IsSupportedImageFormat(string fileExtension)
    {
        string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        return supportedExtensions.Contains(fileExtension.ToLower());
    }
    
    private void DeleteImage(string? filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    public async Task<string> ReplaceImage(string? existImagePath, IFormFile? newImage, string pathToUpload)
    {
        if (existImagePath != null || existImagePath != "") 
            DeleteImage(existImagePath);
        var newImagePath = await SaveImage(newImage, pathToUpload);
        return newImagePath;
    }
}
