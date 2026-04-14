using Microsoft.AspNetCore.Authorization;

namespace InterviewCoach.API.Authorization
{
    // ─────────────────────────────────────────────────────────────
    // REQUIREMENT
    // Carries the data the policy needs to evaluate.
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Requires the authenticated user to be at least <see cref="MinimumAge"/> years old,
    /// derived from a "birthdate" claim (ISO 8601 date) in the Keycloak JWT.
    ///
    /// To add the claim: Keycloak → Client → Client Scopes → Add mapper → User Attribute
    /// Attribute: birthdate | Token Claim Name: birthdate | Type: String
    /// </summary>
    public sealed class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(minimumAge);
            MinimumAge = minimumAge;
        }
    }

    // ─────────────────────────────────────────────────────────────
    // HANDLER
    // Contains the actual evaluation logic.
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Evaluates <see cref="MinimumAgeRequirement"/> against the current user's JWT claims.
    /// </summary>
    public sealed class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeHandler> _logger;

        public MinimumAgeHandler(ILogger<MinimumAgeHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumAgeRequirement requirement)
        {
            var subject = context.User.FindFirst("sub")?.Value ?? "unknown";

            var birthdateClaim = context.User.FindFirst("birthdate");

            if (birthdateClaim is null)
            {
                _logger.LogWarning(
                    "MinimumAge authorization failed for sub={Subject}: 'birthdate' claim missing from token.",
                    subject);
                context.Fail(new AuthorizationFailureReason(this, "Missing 'birthdate' claim in token."));
                return Task.CompletedTask;
            }

            if (!DateOnly.TryParse(birthdateClaim.Value, out var birthdate))
            {
                _logger.LogWarning(
                    "MinimumAge authorization failed for sub={Subject}: cannot parse birthdate '{Value}'.",
                    subject, birthdateClaim.Value);
                context.Fail(new AuthorizationFailureReason(this, "Invalid 'birthdate' claim format."));
                return Task.CompletedTask;
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;

            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning(
                    "MinimumAge authorization failed for sub={Subject}: age {Age} < required {Required}.",
                    subject, age, requirement.MinimumAge);
                context.Fail(new AuthorizationFailureReason(
                    this, $"User must be at least {requirement.MinimumAge} years old."));
            }

            return Task.CompletedTask;
        }
    }
}
