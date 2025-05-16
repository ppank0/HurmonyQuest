using ContestService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Text.Json;

namespace ContestService.DAL.Repositories.Implementations;
public class CachedRepositoryDecorator<T>(IRepositoryBase<T> inner, IDistributedCache cache, ILogger<CachedRepositoryDecorator<T>> logger) : IRepositoryBase<T> where T : class
{
    public async Task<List<T>> GetAllToListAsync(CancellationToken ct)
    {
        var cacheKey = $"{typeof(T).Name}_GetAllAsync";

        try
        {
            string? cachedData = await cache.GetStringAsync(cacheKey, ct);
            if (cachedData is not null)
            {
                return JsonSerializer.Deserialize<List<T>>(cachedData)!;
            }

            var result = await inner.GetAllToListAsync(ct);

            await cache.SetStringAsync(cacheKey,
                JsonSerializer.Serialize(result),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                }, ct);

            return result;
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "Redis is unavailable. Falling back to DB for type {type}", typeof(T).Name);

            return await inner.GetAllToListAsync(ct);
        }
    }
    public async Task<T> CreateAsync(T entity, CancellationToken ct)
       => await inner.CreateAsync(entity, ct);

    public async Task DeleteAsync(T entity, CancellationToken ct)
        => await inner.DeleteAsync(entity, ct);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct)
        => inner.FindByCondition(expression, ct);

    public IQueryable<T> GetAll(CancellationToken ct)
        => inner.GetAll(ct);

    public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken ct)
        => await inner.FindByConditionAsync(expression, ct);

    public async Task<T> UpdateAsync(T entity, CancellationToken ct)
        => await inner.UpdateAsync(entity, ct);
}
