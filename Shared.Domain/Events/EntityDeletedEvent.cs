namespace Shared.Domain.Events
{
    public class EntityDeletedEvent<TKey> : BaseDomainEvent
    {
        public TKey EntityId { get; }
        public string EntityType { get; }

        public EntityDeletedEvent(TKey entityId, string entityType, DateTime occurredOn)
            : base(occurredOn)
        {
            EntityId = entityId;
            EntityType = entityType;
        }
    }
}
