using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Abstractions.Events;

namespace Shared.Application.Events
{
    public class ApplicationEventDispatcher : IApplicationEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task DispatchAsync<TEvent>(TEvent appEvent, CancellationToken cancellationToken = default)
            where TEvent : IApplicationEvent
        {
            var handlers = _serviceProvider.GetServices<IApplicationEventHandler<TEvent>>().ToList();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(appEvent, cancellationToken);
            }
        }
    }
}
