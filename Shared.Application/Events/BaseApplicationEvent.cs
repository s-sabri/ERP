using Shared.Application.Abstractions.Events;

namespace Shared.Application.Events
{
    public abstract class BaseApplicationEvent : IApplicationEvent
    {
        public DateTime OccurredOn { get; protected set; }

        protected BaseApplicationEvent(DateTime occurredOn)
        {
            OccurredOn = occurredOn;
        }
    }
}
