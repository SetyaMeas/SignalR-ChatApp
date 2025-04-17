using ChatApp.Application.Commons.Interfaces;
using ChatApp.Infrastucture.Cache;
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
            builder.Services.AddRedisService(config);
            builder.Services.AddDIService();
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

        private static IServiceCollection AddRedisService(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration =
                    config.GetConnectionString("Redis")
                    ?? throw new ArgumentException("Redis connection string is missing");
            });
            return services;
        }

        private static IServiceCollection AddDIService(this IServiceCollection services)
        {
            services.AddSingleton<ICachingRepository, CachingRepository>();
            services.AddSingleton<IRegisterCaching, RegisterCaching>();
            return services;
        }
    }
}
