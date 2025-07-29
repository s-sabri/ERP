using Microsoft.EntityFrameworkCore;
using Shared.Domain.Specifications;

namespace Shared.Infrastructure.EFCore.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification) where T : class
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
