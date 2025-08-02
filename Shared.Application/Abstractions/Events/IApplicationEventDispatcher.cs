namespace Shared.Application.Abstractions.Events
{
    public interface IApplicationEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent appEvent, CancellationToken cancellationToken = default) where TEvent : IApplicationEvent;
    }
}
