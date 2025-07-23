using System.Linq.Expressions;
using Shared.Domain.Abstractions;
using Shared.Domain.Entities;
using Shared.Domain.Common;

namespace Shared.Domain.Repositories
{
    public interface IBaseRepository<TEntity, TKey> where TKey : notnull where TEntity : BaseEntity<TKey>, IAggregateRoot<TKey>, new()
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task AddRangeTransactionalAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateRangeTransactionalAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeTransactionalAsync(IEnumerable<TEntity> entities);
        Task DeleteByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        Task<Paginated<TEntity>> GetAllPaginatedAsync(int page, int pageSize, bool asNoTracking = false);
        Task<IEnumerable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, bool asNoTracking = false);
        Task<Paginated<TEntity>> GetByExpressionPaginatedAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, bool asNoTracking = false);
        Task<IEnumerable<TResult>> GetProjectedByExpressionAsync<TResult>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, bool asNoTracking = false);
        Task<Paginated<TResult>> GetProjectedByExpressionPaginatedAsync<TResult>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector, int page, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, bool asNoTracking = false);
        Task<TEntity?> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, bool asNoTracking = false);
        Task<TKey> GetMaxIdAsync();
    }
}
