using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public class DomainOrSpecification<T> : DomainSpecification<T>
    {
        private readonly DomainSpecification<T> _left;
        private readonly DomainSpecification<T> _right;

        public DomainOrSpecification(DomainSpecification<T> left, DomainSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpr = _left.ToExpression();
            var rightExpr = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.OrElse(
                Expression.Invoke(leftExpr, parameter),
                Expression.Invoke(rightExpr, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
