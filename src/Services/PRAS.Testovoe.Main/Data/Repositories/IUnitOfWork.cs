using PRAS.Testovoe.Main.Models;
using Raf.Infrastructure.Shared.Repositories;

namespace PRAS.Testovoe.Main.Data.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    IRepository<RefreshToken, int> RefreshTokens { get; }
    IRepository<News, int> News { get; }
}