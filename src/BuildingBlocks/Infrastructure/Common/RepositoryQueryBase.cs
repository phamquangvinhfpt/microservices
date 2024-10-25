using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class RepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K, TContext>
    where T : EntityBase<K>
    where TContext : DbContext
{
    protected readonly TContext _context;

    public RepositoryQueryBase(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindAll(trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProterty) => current.Include(includeProterty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
        !trackChanges 
            ? _context.Set<T>().Where(expression).AsNoTracking() 
            : _context.Set<T>().Where(expression);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProterty) => current.Include(includeProterty));
        return items;
    }

    public async Task<T?> GetByIdAsync(K id) => await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();

    public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
        await FindByCondition(x => x.Id.Equals(id), false, includeProperties).FirstOrDefaultAsync();
}