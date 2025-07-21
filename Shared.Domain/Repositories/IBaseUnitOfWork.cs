using Shared.Domain.Events;

namespace Shared.Domain.Repositories
{
    public interface IBaseUnitOfWork
    {
        Task SaveChangesAsync(IEnumerable<IDomainEvent>? domainEvents = null);
    }
}
