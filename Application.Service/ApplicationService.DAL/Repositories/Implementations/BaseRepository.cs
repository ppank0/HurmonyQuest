using ApplicationService.DAL.Context;
using ApplicationService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationService.DAL.Repositories.Implementations
{
    public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context = context;
        public async Task CreateAsync(TEntity entity, CancellationToken ct)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken ct)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, CancellationToken ct)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Set<TEntity>().ToListAsync(ct);
        }

        public Task UpdateAsync(TEntity entity, CancellationToken ct)
        {
            _context.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

    }
}
