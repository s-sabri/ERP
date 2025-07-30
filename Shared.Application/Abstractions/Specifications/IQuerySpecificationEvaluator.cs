using Shared.Domain.Specifications;

namespace Shared.Application.Abstractions.Specifications
{
    public interface IQuerySpecificationEvaluator<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> query, IQuerySpecification<TEntity> specification);
    }
}
