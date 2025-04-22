using ContestService.DAL.Context;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContestService.DAL.Repositories.Implementations;

public class RepositoryBase<T>(AppDbContext context) : IRepositoryBase<T> where T : class
{
    protected readonly AppDbContext _context = context;

    public async Task<T> CreateAsync(T entity, CancellationToken ct)
    {
        var created =_context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(ct);
        return created.Entity;
    }

    public async Task DeleteAsync(T entity, CancellationToken ct)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(ct);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct)
    {
        return _context.Set<T>().Where(expression).AsNoTracking();
    }

    public IQueryable<T> GetAll(CancellationToken ct)
    {
        return _context.Set<T>().AsNoTracking();
    }

    public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken ct)
    {
        return await _context.Set<T>().Where(expression).ToListAsync(ct);
    }
    public async Task<T> UpdateAsync(T entity, CancellationToken ct)
    {
        var updated = _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(ct);
        return updated.Entity;
    }

    public async Task<List<T>> GetAllToListAsync(CancellationToken ct)
    {
        return await _context.Set<T>().ToListAsync(ct);
    }
}
