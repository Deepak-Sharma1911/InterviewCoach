using InterviewCoach.API.Middleware;

namespace InterviewCoach.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlingMiddleware>();
            return builder;
        }
        public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestContextLoggingMiddleware>();
            return builder;
        }
    }
}
