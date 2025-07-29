namespace Shared.Domain.Events
{
    public class EntityHardDeletedEvent<TKey> : BaseDomainEvent
    {
        public TKey EntityId { get; }
        public string EntityType { get; }

        public EntityHardDeletedEvent(TKey entityId, string entityType, DateTime occurredOn)
            : base(occurredOn)
        {
            EntityId = entityId;
            EntityType = entityType;
        }
    }
}
