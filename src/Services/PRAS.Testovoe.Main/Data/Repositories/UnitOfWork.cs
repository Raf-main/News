

using Microsoft.EntityFrameworkCore;
using PRAS.Testovoe.Main.Data.Contexts;
using PRAS.Testovoe.Main.Models;
using Raf.Infrastructure.Shared.Repositories;

namespace PRAS.Testovoe.Main.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IRepository<News, int> News => _newsRepository;
    public IRepository<RefreshToken, int> RefreshTokens => _refreshTokenRepository;

    private readonly DbContext _dbContext;
    private readonly IRepository<News,int> _newsRepository;
    private readonly IRepository<RefreshToken,int> _refreshTokenRepository;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(NewsDbContext newsDbContext, ILogger<UnitOfWork> logger)
    {
        _dbContext = newsDbContext;
        _logger = logger;   
        _newsRepository = new Repository<News,int>(_dbContext);
        _refreshTokenRepository = new Repository<RefreshToken,int>(_dbContext);
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Changes were successfully saved to the database");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Saving changes to the database passed with error");

            throw;
        }
    }
}