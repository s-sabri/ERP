using System.Linq.Expressions;

namespace Shared.Application.Abstractions.Specifications
{
    public interface IQuerySpecification<TEntity>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        bool AsNoTracking { get; }
        int? Skip { get; }
        int? Take { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
    }
}
