using Microsoft.EntityFrameworkCore;
using Shared.Application.Contracts.Services;

namespace Shared.Infrastructure.EFCore.Specifications
{
    public class EfCoreQueryableService<TEntity> : IQueryableService<TEntity> where TEntity : class
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;
        public EfCoreQueryableService(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _context = context;
        }
        public IQueryable<TEntity> Query(bool asNoTracking = true)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            return asNoTracking ? query.AsNoTracking() : query;
        }
    }
}
