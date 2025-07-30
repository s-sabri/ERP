using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public abstract class DomainSpecification<TEntity> : IDomainSpecification<TEntity>
    {
        private readonly List<Expression<Func<TEntity, object>>> _includes = new();
        public virtual List<Expression<Func<TEntity, object>>> Includes => _includes;
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }
        protected void AddInclude<TProperty>(Expression<Func<TEntity, TProperty>> includeExpression)
        {
            Expression converted = Expression.Convert(includeExpression.Body, typeof(object));
            var casted = Expression.Lambda<Func<TEntity, object>>(converted, includeExpression.Parameters);

            _includes.Add(casted);
        }

        public DomainSpecification<TEntity> And(DomainSpecification<TEntity> other) => new DomainAndSpecification<TEntity>(this, other);
        public DomainSpecification<TEntity> Or(DomainSpecification<TEntity> other) => new DomainOrSpecification<TEntity>(this, other);
        public DomainSpecification<TEntity> Not() => new DomainNotSpecification<TEntity>(this);
    }
}
