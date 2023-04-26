using Raf.Utils.Shared.Time;

namespace PRAS.Testovoe.Main.Services.Image;

public class ImageService : IImageService
{
    const string imageFolderName = "Images"; 
    private readonly IWebHostEnvironment _environment;
    private readonly IDateTimeProvider _dateTimeProvider;
    public ImageService(IWebHostEnvironment environment, IDateTimeProvider dateTimeProvider)
    {
        _environment = environment;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<string> StoreImageAsync(IFormFile img)
    {
        var imageExtension = Path.GetExtension(img.FileName);
        var utcYear = _dateTimeProvider.UtcNow().Year;
        var utcDay = _dateTimeProvider.UtcNow().Day;

        string uniqueImageName = $"{Guid.NewGuid().ToString()}_{utcYear}_{utcDay}{imageExtension}";  
        var directoryPath = GetDirectoryPath();
        
        if(!Directory.Exists(directoryPath)) 
        {
            Directory.CreateDirectory(directoryPath);
        }

        string imagePath = GetImagePath(uniqueImageName);  
        
        using (var fileStream = new FileStream(imagePath, FileMode.Create))  
        {
            await img.CopyToAsync(fileStream);  
        }

        return uniqueImageName;
    }

    public string GetImagePath(string imageName) 
    {
        return Path.Combine(GetDirectoryPath(), imageName);
    }

    public string GetDirectoryPath()
    {
        return Path.Combine(_environment.WebRootPath, imageFolderName);
    }

    public void DeleteImage(string imageName)
    {
        File.Delete(GetImagePath(imageName));
    }
}