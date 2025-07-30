namespace Shared.Application.Contracts.Services
{
    public interface IQueryableService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query(bool asNoTracking = true);
    }
}
