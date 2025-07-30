using System.Linq.Expressions;

namespace Shared.Application.Abstractions.Specifications
{
    public abstract class QuerySpecification<TEntity> : IQuerySpecification<TEntity>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
        public bool AsNoTracking { get; protected set; } = true;
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }
        public Expression<Func<TEntity, object>>? OrderBy { get; protected set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; protected set; }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
        protected void ApplyTracking(bool asNoTracking)
        {
            AsNoTracking = asNoTracking;
        }
        protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
