using System.Linq.Expressions;

namespace ContestService.DAL.Repositories.Interfaces;

public interface IRepositoryBase<T>
{
    IQueryable<T> GetAll(CancellationToken ct);
    Task<List<T>> GetAllToListAsync(CancellationToken ct);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct);
    Task<List<T>> FindByConditionToListAsync(Expression<Func<T, bool>> expression, CancellationToken ct);
    Task<T> CreateAsync(T entity, CancellationToken ct);
    Task<T> UpdateAsync(T entity, CancellationToken ct);
    Task DeleteAsync(T entity, CancellationToken ct);
}
