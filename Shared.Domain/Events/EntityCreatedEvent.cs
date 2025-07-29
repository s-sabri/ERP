namespace Shared.Domain.Events
{
    public class EntityCreatedEvent<TKey> : BaseDomainEvent
    {
        public TKey EntityId { get; }
        public string EntityType { get; }

        public EntityCreatedEvent(TKey entityId, string entityType, DateTime occurredOn)
            : base(occurredOn)
        {
            EntityId = entityId;
            EntityType = entityType;
        }
    }
}
