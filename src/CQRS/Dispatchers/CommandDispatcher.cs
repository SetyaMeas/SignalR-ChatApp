using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> DispatchAsync<TResult>(
            ICommand<TResult> command,
            CancellationToken cancellationToken = default
        )
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(
                command.GetType(),
                typeof(TResult)
            );

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return handler.HandleAsync((dynamic)command, cancellationToken);
        }
    }
}
