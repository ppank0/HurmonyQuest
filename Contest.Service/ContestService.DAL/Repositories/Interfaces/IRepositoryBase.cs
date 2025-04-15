using System.Linq.Expressions;

namespace ContestService.DAL.Repositories.Interfaces;

internal interface IRepositoryBase<T>
{
    IQueryable<T> GetAll(CancellationToken ct);
    Task<List<T>> GetAllToListAsync(CancellationToken ct);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct);
    Task<List<T>> FindByConditionToListAsync(Expression<Func<T, bool>> expression, CancellationToken ct);
    Task CreateAsync(T entity, CancellationToken ct);
    Task UpdateAsync(T entity, CancellationToken ct);
    Task DeleteAsync(T entity, CancellationToken ct);
}
