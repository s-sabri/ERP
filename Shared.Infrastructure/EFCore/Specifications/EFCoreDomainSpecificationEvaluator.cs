using Microsoft.EntityFrameworkCore;
using Shared.Domain.Specifications;

namespace Shared.Infrastructure.EFCore.Specifications
{
    public static class EFCoreDomainSpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, IDomainSpecification<TEntity> specification) where TEntity : class
        {
            var query = inputQuery;

            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            return query.Where(specification.ToExpression());
        }
    }
}
