using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InterviewCoach.API.Extensions
{
    /// <summary>
    /// Removes the JWT security requirement from endpoints decorated with [AllowAnonymous].
    /// This means the lock icon won't appear on public endpoints like /api/auth/login,
    /// making it clear to API consumers that no token is needed to call them.
    /// </summary>
    public class AnonymousEndpointOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check for [AllowAnonymous] on the action method or its declaring controller
            var hasAllowAnonymous = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>()
                .Any()
                ||
                context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>()
                .Any() == true;

            if (hasAllowAnonymous)
            {
                operation.Security.Clear();
            }
        }
    }
}
