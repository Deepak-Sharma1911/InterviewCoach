using InterviewCoach.API.Extensions;
using InterviewCoach.API.Infrastructure;
using InterviewCoach.API.Services;
using InterviewCoach.Application;
using InterviewCoach.Application.Abstractions;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace InterviewCoach.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            // ── 1. Bind strongly-typed Keycloak settings ────────────────────────────────
            services.Configure<KeycloakOptions>(
                configuration.GetSection(KeycloakOptions.SectionName));

            var keycloakOptions = configuration.GetSection(KeycloakOptions.SectionName)
                                               .Get<KeycloakOptions>() ?? throw new InvalidOperationException(
                                                $"Missing '{KeycloakOptions.SectionName}' configuration section.");

            services.AddKeycloakAuthentication(keycloakOptions);
            // ── 3. Authorization — role-based policies + custom requirement policies ─────
            services.AddKeycloakAuthorization();
            // ── 4. HttpClient for any outbound calls to Keycloak (token proxy, etc.) ────
            services.AddHttpClient("keycloak", client =>
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters
                          .Add(new JsonStringEnumConverter());
                    });
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddSwaggerGenWithAuth();
            services.AddCors();        
            return services;
        }
        public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API",
                    Version = "v1",
                    Description = """
                    Secured with Keycloak JWT Bearer authentication.

                    **How to authenticate:**
                    1. Use **POST /api/auth/login** with your username and password
                    2. Copy the `accessToken` from the response
                    3. Click **Authorize** (🔒) at the top of this page
                    4. Paste the token — Swagger adds the `Bearer ` prefix automatically
                    5. Click **Authorize** then **Close**

                    All protected endpoints will now send the token on every request.
                    When your token expires, call **POST /api/auth/refresh** with the `refreshToken`.
                    """
                });

                // JWT Bearer scheme — shows the lock icon on each endpoint
                var jwtScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Paste your JWT access token here. Do NOT include the 'Bearer ' prefix — Swagger adds it automatically.",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", jwtScheme);

                // Apply the scheme globally — every endpoint shows the lock icon
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtScheme, Array.Empty<string>() }
            });

                // Show [AllowAnonymous] endpoints without the lock icon
                options.OperationFilter<AnonymousEndpointOperationFilter>();
            });
            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
