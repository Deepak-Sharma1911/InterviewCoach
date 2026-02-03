using InterviewCoach.API.Infrastructure;
using InterviewCoach.API.Middleware;

namespace InterviewCoach.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestContextLoggingMiddleware>();
            return builder;
        }
    }
}
