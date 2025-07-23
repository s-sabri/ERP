using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Abstractions
{
    public interface IAggregateRoot<TKey> : IBaseEntity<TKey> where TKey : notnull
    {
    }
}
