namespace Shared.Domain.Events
{
    public class EntityUpdatedEvent<TKey> : BaseDomainEvent
    {
        public TKey EntityId { get; }
        public string EntityType { get; }

        public EntityUpdatedEvent(TKey entityId, string entityType, DateTime occurredOn)
            : base(occurredOn)
        {
            EntityId = entityId;
            EntityType = entityType;
        }
    }
}
