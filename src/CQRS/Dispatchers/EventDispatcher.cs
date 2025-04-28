using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task DispatchAsync(IEvent ievent, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(ievent.GetType());
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);

            return handler.HandleAsync((dynamic)ievent, cancellationToken);
        }
    }
}
