namespace Shared.Domain.Events
{
    public abstract class BaseDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; protected set; }

        protected BaseDomainEvent(DateTime occurredOn)
        {
            OccurredOn = occurredOn;
        }
    }
}
