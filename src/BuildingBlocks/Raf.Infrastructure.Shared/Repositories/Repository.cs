using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Raf.Infrastructure.Shared.SpecificationPattern;

namespace Raf.Infrastructure.Shared.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IHasKey<TKey>
{
    protected readonly DbSet<TEntity> Table;

    public Repository(DbContext context)
    {
        Table = context.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
    }

    public virtual async Task<TEntity?> GetAsync(TKey key, bool asTracking = false)
    {
        var query = asTracking ? Table.AsTracking() : Table.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Id!.Equals(key));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Specification<TEntity>? spec = null, bool asTracking = false)
    {
        var query = asTracking ? Table.AsTracking() : Table.AsNoTracking();

        query = ApplySpecification(query, spec);

        return await query.ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetPagedAsync<TOrdeyType>(
        int page, 
        int size, 
        Expression<Func<TEntity, TOrdeyType>>? orderBy = null, 
        Specification<TEntity>? spec = null, 
        bool orderByDesc = false,
        bool asTracking = false)
    {
        var query = asTracking ? Table.AsTracking() : Table.AsNoTracking();

        if(orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        query = ApplySpecification(query, spec);

        return await query.Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        Table.Entry(entity).State = EntityState.Modified;

        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(TKey key)
    {
        var entityToDelete = await GetAsync(key, true);
        
        if (entityToDelete != null)
        {
            Table.Entry(entityToDelete).State = EntityState.Deleted;
        }
    }

    private static IQueryable<TEntity> OrderBy<TOrdeyType>(IQueryable<TEntity> query, Expression<Func<TEntity, TOrdeyType>> orderBy, bool orderyByDesc)
    {
        if(orderyByDesc)
        {
            return query.OrderByDescending(orderBy);
        }

        return query.OrderBy(orderBy);
    }

    private static IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> query, Specification<TEntity>? spec = null)
    {
        if (spec == null)
        {
            return query;
        }

        return query.Where(spec.ToExpression());
    }
}