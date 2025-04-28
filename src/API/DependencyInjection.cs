using System.Diagnostics;
using System.Text;
using ChatApp.API.Filters;
using ChatApp.API.Middlewares;
using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddAPIServices(
            this IHostApplicationBuilder builder,
            IConfiguration config
        )
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddProblemDetailService();
            builder.Services.AddDIService();
            builder.Services.AddJwtService(config);

            builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
        }

        private static IServiceCollection AddDIService(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilter>();
            return services;
        }

        private static IServiceCollection AddProblemDetailService(this IServiceCollection services)
        {
            services.AddProblemDetails(opt =>
            {
                opt.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Instance =
                        $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                    context.ProblemDetails.Extensions.TryAdd("timestampz", DateTime.UtcNow);

                    Activity? activity = context
                        .HttpContext.Features.Get<IHttpActivityFeature>()
                        ?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                };
            });
            return services;
        }

        private static void AddJwtService(this IServiceCollection services, IConfiguration config)
        {
            string jwtSecretKey =
                config["Jwt:Secret"]
                ?? throw new ArgumentNullException("[JWT Configuration Error] Secret key is missing");

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false; // should use default value "true" in production
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(jwtSecretKey)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Request.Cookies.TryGetValue(
                                CookieEnum.ACCESS_TOKEN.ToString(),
                                out var accessToken
                            );

                            if (accessToken is null)
                            {
                                return Task.CompletedTask;
                            }

                            context.Token = accessToken;
                            return Task.CompletedTask;
                        },
                    };
                });
        }
    }
}
