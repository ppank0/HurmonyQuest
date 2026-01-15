using System.Linq.Expressions;

namespace ApplicationService.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct);
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, CancellationToken ct);
        Task UpdateAsync(TEntity entity, CancellationToken ct);
        Task DeleteAsync(TEntity entity, CancellationToken ct);
        Task CreateAsync(TEntity entity, CancellationToken ct);
    }
}
