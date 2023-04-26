using System.Linq.Expressions;
using Raf.Infrastructure.Shared.SpecificationPattern;

namespace Raf.Infrastructure.Shared.Repositories;

public interface IRepository<TEntity, TKey> where TEntity : class, IHasKey<TKey>
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> GetAsync(TKey key, bool asTracking = false);
    
    Task <IEnumerable<TEntity>> GetAsync(
        Specification<TEntity>? spec = null, 
        bool asTracking = false
    );

    Task<IEnumerable<TEntity>> GetPagedAsync<TOrderBy>(
        int page, 
        int size, 
        Expression<Func<TEntity, TOrderBy>>? orderBy,
        Specification<TEntity>? spec = null,
        bool orderByDesc = false,
        bool asTracking = false
    );

    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey key);
}