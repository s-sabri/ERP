using Shared.Application.Common;
using Shared.Domain.Abstractions;
using Shared.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Shared.Application.Contracts.Services
{
    public interface IEntityGraphProcessor<TEntity, TKey> where TKey : notnull where TEntity : BaseEntity<TKey>, IAggregateRoot<TKey>
    {
        ChangeLog ProcessGraphAsync(TEntity newEntity, TEntity? oldEntity, PersianDateTime persianDateTime, EntityOperation operation);
    }
}
