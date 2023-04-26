namespace PRAS.Testovoe.Main.Services.Image;

public interface IImageService
{
    Task<string> StoreImageAsync(IFormFile img);
    string GetImagePath(string imageName) ;
    string GetDirectoryPath();
    void DeleteImage(string imageName);
}