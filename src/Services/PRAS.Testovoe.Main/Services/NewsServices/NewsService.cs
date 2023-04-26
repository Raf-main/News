using System.Linq.Expressions;
using PRAS.Testovoe.Main.Data.Repositories;
using PRAS.Testovoe.Main.Exceptions;
using PRAS.Testovoe.Main.Models;
using PRAS.Testovoe.Main.Services.Image;
using PRAS.Testovoe.Main.ViewModels.Request;

namespace PRAS.Testovoe.Main.Services.NewsServices;

public class NewsService : INewsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    public NewsService(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task CreateAsync(CreateNewsRequest request)
    {
        var imageName = await _imageService.StoreImageAsync(request.Image);

        var news = new News
        {
          Title = request.Title,
          Subtitle = request.Subtitle,
          Text = request.Text,
          ImageName = imageName,
          CreatedOn = DateTime.Now,
        };

        await _unitOfWork.News.AddAsync(news);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<News?> GetAsync(int id)
    {
        return await _unitOfWork.News.GetAsync(id);
    }

    public async Task<IEnumerable<News>> GetPagedAsync(int page, int size)
    {
        if(page < 1)
        {
            page = 1;
        }

        if(size < 1)
        {
            size = 30;
        }

        Expression<Func<News, DateTime>> orderBy = n => n.CreatedOn;

        return await _unitOfWork.News.GetPagedAsync<DateTime>(page, size, orderBy, orderByDesc: true);
    }

    public async Task UpdateAsync(int id, UpdateNewsRequest requests)
    {
        var news = await GetAsync(id);

        if (news == null) 
        {
            throw new NotFoundException();
        }

        news.Text = requests.Text;
        news.Title = requests.Title;
        news.Subtitle = requests.Subtitle;

        await _unitOfWork.News.UpdateAsync(news);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateImageAsync(int id, UpdateNewsImageRequest request)
    {
        var news = await GetAsync(id);

        if (news == null) 
        {
            throw new NotFoundException();
        }

        var imageName = await _imageService.StoreImageAsync(request.Image);
        news.ImageName = imageName;
        
        await _unitOfWork.News.UpdateAsync(news);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.News.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}