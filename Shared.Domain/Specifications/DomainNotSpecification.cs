using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public class DomainNotSpecification<T> : DomainSpecification<T>
    {
        private readonly DomainSpecification<T> _spec;

        public DomainNotSpecification(DomainSpecification<T> spec)
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
