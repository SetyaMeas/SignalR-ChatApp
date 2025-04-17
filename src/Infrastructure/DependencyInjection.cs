using ChatApp.Application.Commons.Interfaces;
using ChatApp.Infrastucture.Cache;
using ChatApp.Infrastucture.Persistence.Context;
using FluentEmail.MailKitSmtp;
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
            builder.Services.AddFluentEmailService(config);
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
                        ?? throw new ArgumentException(
                            "[ConnectionStrings Configuration Error] SQL Server connection is missing"
                        )
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
                    ?? throw new ArgumentException(
                        "[ConnectionStrings Configuration Error] Redis connection is missing"
                    );
            });
            return services;
        }

        private static IServiceCollection AddFluentEmailService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddFluentEmail(
                    configuration["Email:UserEmail"]
                        ?? throw new ArgumentException(
                            "[Email Configuration Error] UserEmail is missing"
                        ),
                    configuration["Email:UserName"]
                        ?? throw new ArgumentException(
                            "[Email Configuration Error] UserName is missing"
                        )
                )
                .AddMailKitSender(
                    new SmtpClientOptions
                    {
                        Server =
                            configuration["Email:Server"]
                            ?? throw new ArgumentException(
                                "[Email Configuration Error] Server is missing"
                            ),
                        Port =
                            configuration.GetValue<int?>("Email:Port")
                            ?? throw new ArgumentException(
                                "[Email Configuration Error] Port is missing"
                            ),
                        Password =
                            configuration["Email:Password"]
                            ?? throw new ArgumentException(
                                "[Email Configuration Error] Password is missing"
                            ),

                        UseSsl =
                            configuration.GetValue<bool?>("Email:UseSsl")
                            ?? throw new ArgumentException(
                                "[Email Configuration Error] UseSsl is missing"
                            ),
                        User = configuration["Email:UserEmail"],
                        RequiresAuthentication = true,
                    }
                );
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
