using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        private readonly List<Expression<Func<T, object>>> _includes = new();
        public virtual List<Expression<Func<T, object>>> Includes => _includes;
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }
        protected void AddInclude<TProperty>(Expression<Func<T, TProperty>> includeExpression)
        {
            Expression converted = Expression.Convert(includeExpression.Body, typeof(object));
            var casted = Expression.Lambda<Func<T, object>>(converted, includeExpression.Parameters);

            _includes.Add(casted);
        }

        public Specification<T> And(Specification<T> other) => new AndSpecification<T>(this, other);
        public Specification<T> Or(Specification<T> other) => new OrSpecification<T>(this, other);
        public Specification<T> Not() => new NotSpecification<T>(this);
    }
}
