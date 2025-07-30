using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public interface IDomainSpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> ToExpression();
        List<Expression<Func<TEntity, object>>> Includes { get; }
        bool IsSatisfiedBy(TEntity entity);
    }
}
