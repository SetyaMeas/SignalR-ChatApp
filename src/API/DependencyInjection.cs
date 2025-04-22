using System.Diagnostics;
using ChatApp.API.Filters;
using ChatApp.API.Middlewares;
using Microsoft.AspNetCore.Http.Features;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddAPIServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddProblemDetailService();
            builder.Services.AddDIService();

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
    }
}
