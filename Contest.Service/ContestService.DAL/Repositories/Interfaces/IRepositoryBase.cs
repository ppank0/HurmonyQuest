using System.Linq.Expressions;

namespace ContestService.DAL.Repositories.Interfaces
{
    internal interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll(CancellationToken ct);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct);
        Task<IEnumerable<T>> FindByConditionToListAsync(Expression<Func<T, bool>> expression, CancellationToken ct);
        Task CreateAsync(T entity, CancellationToken ct);
        Task UpdateAsync(T entity, CancellationToken ct);
        Task DeleteAsync(T entity, CancellationToken ct);
    }
}
