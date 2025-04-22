using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Commons
{
    public class ApiErrorResponse : ProblemDetails
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Errors { get; set; }

        public ApiErrorResponse(HttpContext context)
        {
            Instance = $"{context.Request.Method} {context.Request.Path}";
            Extensions.TryAdd("timestampz", DateTime.UtcNow);

            Activity? activity = context.Features.Get<IHttpActivityFeature>()?.Activity;
            Extensions.TryAdd("traceId", activity?.Id);
        }
    }
}
