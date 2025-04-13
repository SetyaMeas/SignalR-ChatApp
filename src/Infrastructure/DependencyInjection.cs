using Application.Commons.Interfaces;
using ChatApp.Infrastucture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IHostApplicationBuilder builder,
            IConfiguration config
        )
        {
            builder.Services.AddDbService(config);
        }

        private static IServiceCollection AddDbService(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("SQL Server")
                        ?? throw new ArgumentException("SQL Server connection string is missing")
                )
            );

            services.AddScoped<IApplicationDbContext, AppDbContext>();
            return services;
        }
    }
}
