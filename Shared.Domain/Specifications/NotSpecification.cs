using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec;

        public NotSpecification(Specification<T> spec)
        {
            _spec = spec;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr = _spec.ToExpression();
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.Not(Expression.Invoke(expr, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
