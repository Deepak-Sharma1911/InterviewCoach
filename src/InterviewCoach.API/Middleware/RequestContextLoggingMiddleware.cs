using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace InterviewCoach.API.Middleware
{
    public class RequestContextLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestContextLoggingMiddleware> _logger;
        public RequestContextLoggingMiddleware(ILogger<RequestContextLoggingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        private const string CorrelationIdHeaderName = "Correlation-Id";
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Request Path: {Path}", context.Request.Path);

            using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
            {
                await _next(context);
            }
        }
        private static string GetCorrelationId(HttpContext context)
        {
            context.Request.Headers.TryGetValue(
                CorrelationIdHeaderName,
                out StringValues correlationId);

            return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
        }
    }
}
