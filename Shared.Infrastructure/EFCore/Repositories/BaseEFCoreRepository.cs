using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using Shared.Domain.Common;
using Shared.Domain.Entities;
using Shared.Domain.Repositories;
using Shared.Domain.Specifications;
using Shared.Infrastructure.EFCore.Specifications;

namespace Shared.Infrastructure.EFCore.Repositories
{
    public class BaseEFCoreRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TKey : notnull where TEntity : BaseEntity<TKey>, IAggregateRoot<TKey>, new()
    {
        protected readonly Microsoft.EntityFrameworkCore.DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public BaseEFCoreRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public async Task AddRangeTransactionalAsync(IEnumerable<TEntity> entities)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }
        public async Task UpdateRangeTransactionalAsync(IEnumerable<TEntity> entities)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _dbSet.UpdateRange(entities);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }
        public async Task DeleteRangeTransactionalAsync(IEnumerable<TEntity> entities)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _dbSet.RemoveRange(entities);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteByIdAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        {
            var query = _dbSet.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
        public async Task<Paginated<TEntity>> GetAllPaginatedAsync(int page, int pageSize, bool asNoTracking = false)
        {
            var totalCount = await _dbSet.CountAsync();
            var query = _dbSet.Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            var result = await query.ToListAsync();
            return new Paginated<TEntity>
            {
                Items = result,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
        public async Task<IEnumerable<TEntity>> GetBySpecificationAsync(IDomainSpecification<TEntity> specification, bool asNoTracking = false)
        {
            var query = EFCoreDomainSpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.ToListAsync();
        }
        public async Task<Paginated<TEntity>> GetBySpecificationPaginatedAsync(IDomainSpecification<TEntity> specification, int page, int pageSize,
            bool asNoTracking = false)
        {
            var query = EFCoreDomainSpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
            if (asNoTracking)
                query = query.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Paginated<TEntity>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
        public async Task<IEnumerable<TResult>> GetProjectedBySpecificationAsync<TResult>(IDomainSpecification<TEntity> specification, Expression<Func<TEntity, TResult>> selector,
            bool asNoTracking = false)
        {
            var query = EFCoreDomainSpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.Select(selector).ToListAsync();
        }
        public async Task<Paginated<TResult>> GetProjectedBySpecificationPaginatedAsync<TResult>(IDomainSpecification<TEntity> specification, Expression<Func<TEntity, TResult>> selector,
            int page, int pageSize, bool asNoTracking = false)
        {
            var query = EFCoreDomainSpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
            if (asNoTracking)
                query = query.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();

            return new Paginated<TResult>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
        public async Task<TEntity?> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, bool asNoTracking = false)
        {
            var query = _dbSet.AsQueryable();

            if (include != null)
                query = include(query);

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id));
        }
        public async Task<TKey> GetMaxIdAsync()
        {
            if (!await _dbSet.AnyAsync())
                return default!;

            return await _dbSet.Select(e => e.Id).MaxAsync();
        }
    }
}
