using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions.Specifications;
using System.Linq.Expressions;

namespace Shared.Infrastructure.EFCore.Specifications
{
    public class EfCoreQuerySpecificationEvaluator<TEntity> : IQuerySpecificationEvaluator<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query, IQuerySpecification<TEntity> specification)
        {
            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            foreach (Expression<Func<TEntity, object>> include in specification.Includes)
                query = query.Include(include);

            if (specification.AsNoTracking)
                query = query.AsNoTracking();

            if (specification.Skip.HasValue)
                query = query.Skip(specification.Skip.Value);

            if (specification.Take.HasValue)
                query = query.Take(specification.Take.Value);

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);
            else if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);

            return query;
        }
    }
}
