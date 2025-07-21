using Shared.Application.Common;
using Shared.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Shared.Application.Contracts.Services
{
    public interface IEntityGraphProcessor<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : notnull
    {
        ChangeLog ProcessGraphAsync(TEntity newEntity, TEntity? oldEntity, PersianDateTime persianDateTime, EntityOperation operation);
    }
}
