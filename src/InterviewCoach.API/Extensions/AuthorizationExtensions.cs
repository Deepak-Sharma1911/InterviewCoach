using InterviewCoach.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace InterviewCoach.API.Extensions
{
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Registers all application authorization policies.
        ///
        /// ROLE-BASED policies use RequireRole() and map directly to Keycloak realm roles.
        /// Apply with: [Authorize(Policy = Policies.AdminOnly)]
        ///         or: [Authorize(Roles = Roles.Admin)]   ← shorter for single-role checks
        ///
        /// POLICY-BASED policies use custom IAuthorizationRequirement + IAuthorizationHandler.
        /// Apply with: [Authorize(Policy = Policies.MinimumAge)]
        /// </summary>
        public static IServiceCollection AddKeycloakAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // ── Role-based policies ──────────────────────────────────────────
                options.AddPolicy(Policies.AdminOnly, policy =>
                    policy
                        .RequireAuthenticatedUser()
                        .RequireRole(Roles.Admin));

                options.AddPolicy(Policies.ManagerOrAdmin, policy =>
                    policy
                        .RequireAuthenticatedUser()
                        .RequireRole(Roles.Manager, Roles.Admin));   // OR logic — any one role passes

                options.AddPolicy(Policies.AuthenticatedUser, policy =>
                    policy.RequireAuthenticatedUser());

                // ── Custom requirement-based policy ──────────────────────────────
                // Uses MinimumAgeRequirement + MinimumAgeHandler (registered below)
                options.AddPolicy(Policies.MinimumAge, policy =>
                    policy
                        .RequireAuthenticatedUser()
                        .AddRequirements(new MinimumAgeRequirement(18)));
            });

            // Register custom requirement handlers
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

            return services;
        }
    }
}
