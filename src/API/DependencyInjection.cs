using ChatApp.API.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddAPIServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDIService();
        }

        private static IServiceCollection AddDIService(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilter>();
            return services;
        }
    }
}
