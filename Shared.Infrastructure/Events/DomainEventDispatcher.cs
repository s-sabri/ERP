using MediatR;
using Shared.Application.Logging.Interfaces;
using Shared.Domain.Events;

namespace Shared.Infrastructure.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IAppLogger<DomainEventDispatcher> _logger;

        public DomainEventDispatcher(IMediator mediator, IAppLogger<DomainEventDispatcher> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                try
                {
                    _logger.LogInformation($"Dispatching domain event: {domainEvent.GetType().Name}");
                    await _mediator.Publish(domainEvent);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception while dispatching {domainEvent.GetType().Name}", ex);
                    throw;
                }
            }

            //foreach (var domainEvent in domainEvents)
            //{
            //    var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            //    var handlers = _serviceProvider.GetServices(handlerType);

            //    foreach (var handler in handlers)
            //    {
            //        var handleMethod = handlerType.GetMethod("HandleAsync");
            //        await (Task)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken });
            //    }
            //}
        }
    }
}
