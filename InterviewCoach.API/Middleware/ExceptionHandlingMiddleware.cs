using InterviewCoach.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InterviewCoach.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> logger;
        private readonly RequestDelegate request;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate request)
        {
            this.logger = logger;
            this.request = request;
        }
        public async Task InvokeAsync(HttpContext httpContext, CancellationToken cancellationToken)
        {
            try
            {
                await request(httpContext);
            }
            catch (Exception exception)
            {

                var status = HttpStatusCode.InternalServerError;
                var problemDetails = new ProblemDetails
                {
                    Instance = httpContext.Request.Path,
                    Title = exception.Message
                };

                switch (exception)
                {
                    case BadRequestException badRequest:
                        status = HttpStatusCode.BadRequest;
                        break;

                    case FluentValidation.ValidationException validationException:
                        status = HttpStatusCode.BadRequest;
                        problemDetails.Extensions["ValidationErrors"] = validationException.Errors;
                        break;

                    case NotFoundException notFoundException:
                        status = HttpStatusCode.NotFound;
                        problemDetails.Extensions["NotFoundItemId"] = notFoundException.ErrorId;
                        break;

                    case NotImplementedException:
                        status = HttpStatusCode.NotImplemented;
                        break;

                    case UnauthorizedAccessException:
                        status = HttpStatusCode.Unauthorized;
                        break;

                    case KeyNotFoundException:
                        status = HttpStatusCode.NotFound;
                        break;

                    case BaseException baseException:
                        status = baseException.StatusCode;
                        break;
                }

                problemDetails.Status = (int)status;
                problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
                problemDetails.Extensions["documentationUrl"] =
                    $"https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-6.0#{(int)status}";
                logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
                httpContext.Response.StatusCode = (int)status;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
