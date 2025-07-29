namespace Shared.Domain.Events
{
    public class EntitySoftDeletedEvent<TKey> : BaseDomainEvent
    {
        public TKey EntityId { get; }
        public string EntityType { get; }

        public EntitySoftDeletedEvent(TKey entityId, string entityType, DateTime occurredOn)
            : base(occurredOn)
        {
            EntityId = entityId;
            EntityType = entityType;
        }
    }
}
