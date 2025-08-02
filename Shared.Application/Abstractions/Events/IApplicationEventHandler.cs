namespace Shared.Application.Abstractions.Events
{
    public interface IApplicationEventHandler<in TEvent> where TEvent : IApplicationEvent
    {
        Task HandleAsync(TEvent appEvent, CancellationToken cancellationToken = default);
    }
}
