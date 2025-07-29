using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
        List<Expression<Func<T, object>>> Includes { get; }
        bool IsSatisfiedBy(T entity);
    }
}
