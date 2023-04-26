using PRAS.Testovoe.Main.Models;
using PRAS.Testovoe.Main.ViewModels.Request;

namespace PRAS.Testovoe.Main.Services.NewsServices;

public interface INewsService
{
    Task CreateAsync(CreateNewsRequest request);
    Task<News?> GetAsync(int id);
    Task UpdateAsync(int id, UpdateNewsRequest request);
    Task UpdateImageAsync(int id, UpdateNewsImageRequest request);
    Task<IEnumerable<News>> GetPagedAsync(int page, int size);
    Task DeleteAsync(int id);
}