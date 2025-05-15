using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> DispatchAsync<TResult>(
            IQuery<TResult> query,
            CancellationToken cancellationToken = default
        )
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(
                query.GetType(),
                typeof(TResult)
            );

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return handler.HandleAsync((dynamic)query, cancellationToken);
        }
    }
}
