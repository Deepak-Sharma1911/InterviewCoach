using InterviewCoach.API.Authorization;
using InterviewCoach.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace InterviewCoach.API.Extensions
{
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Registers JWT Bearer authentication configured to validate tokens issued by Keycloak.
        ///
        /// How it works:
        ///   1. On startup, .NET fetches Keycloak's OpenID Connect discovery document from
        ///      {Authority}/.well-known/openid-configuration to get the JWKS endpoint.
        ///   2. On each request, the JWT signature is verified against those public keys.
        ///   3. Standard claims (iss, exp, aud) are validated automatically.
        ///   4. KeycloakRolesClaimsTransformer then maps the "roles" array to ClaimTypes.Role.
        /// </summary>
        public static IServiceCollection AddKeycloakAuthentication(
            this IServiceCollection services,
            KeycloakOptions options)
        {
            services
                .AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    jwt.Authority = options.Authority;
                    jwt.Audience = options.Audience;
                    jwt.RequireHttpsMetadata = options.RequireHttpsMetadata;

                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = options.Authority,

                        // Keycloak places the audience inside resource_access, not the top-level aud.
                        // Set to false here and enforce it via the policy layer instead.
                        ValidateAudience = false,

                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        // Tell .NET which claim holds the user's name and roles
                        NameClaimType = "preferred_username",
                        RoleClaimType = ClaimTypes.Role     // must match what the transformer writes
                    };

                    jwt.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            var logger = ctx.HttpContext.RequestServices
                                .GetRequiredService<ILogger<JwtBearerEvents>>();
                            logger.LogWarning(ctx.Exception,
                                "JWT authentication failed: {Message}", ctx.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnForbidden = ctx =>
                        {
                            var logger = ctx.HttpContext.RequestServices
                                .GetRequiredService<ILogger<JwtBearerEvents>>();
                            logger.LogWarning("Forbidden: user lacks required role/policy.");
                            return Task.CompletedTask;
                        }
                    };
                });

            // Bridges Keycloak's "roles" JSON array → ClaimTypes.Role on every request
            services.AddSingleton<IClaimsTransformation, KeycloakRolesClaimsTransformer>();

            return services;
        }
    }
}
