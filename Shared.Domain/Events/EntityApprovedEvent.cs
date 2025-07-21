namespace Shared.Domain.Events
{
    public class EntityApprovedEvent<TKey> : BaseDomainEvent
    {
        public TKey EntityId { get; }
        public string EntityType { get; }
        public int ApprovalStep { get; }

        public EntityApprovedEvent(TKey entityId, string entityType, int step, DateTime OccurredOn) : base(OccurredOn)
        {
            EntityId = entityId;
            EntityType = entityType;
            ApprovalStep = step;
        }
    }
}
