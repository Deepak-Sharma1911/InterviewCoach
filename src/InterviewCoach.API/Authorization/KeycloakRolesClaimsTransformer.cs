using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;

namespace InterviewCoach.API.Authorization
{
    /// <summary>
    /// Keycloak stores realm roles in the JWT as a JSON array under the "roles" claim,
    /// e.g.: "roles": ["Admin", "User"]
    ///
    /// .NET's [Authorize(Roles = "...")] checks ClaimTypes.Role, not "roles".
    /// This transformer bridges the gap by reading the Keycloak "roles" array
    /// and registering each entry as a standard ClaimTypes.Role claim.
    ///
    /// Registered as a singleton in AuthorizationExtensions.
    /// Runs automatically after every successful token validation.
    /// </summary>
    public sealed class KeycloakRolesClaimsTransformer : IClaimsTransformation
    {
        private const string KeycloakRolesClaim = "roles";
        private readonly ILogger<KeycloakRolesClaimsTransformer> _logger;

        public KeycloakRolesClaimsTransformer(ILogger<KeycloakRolesClaimsTransformer> logger)
        {
            _logger = logger;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;

            if (identity is null || !identity.IsAuthenticated)
                return Task.FromResult(principal);

            // IClaimsTransformation can be called multiple times per request — guard against duplicates
            if (identity.HasClaim(c => c.Type == ClaimTypes.Role))
                return Task.FromResult(principal);

            var rolesClaim = identity.FindFirst(KeycloakRolesClaim);

            if (rolesClaim is null)
            {
                _logger.LogDebug(
                    "No '{Claim}' claim found in token. Ensure the Keycloak realm-roles protocol mapper is enabled.",
                    KeycloakRolesClaim);
                return Task.FromResult(principal);
            }

            try
            {
                var roles = JsonSerializer.Deserialize<List<string>>(rolesClaim.Value);

                if (roles is { Count: > 0 })
                {
                    foreach (var role in roles)
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));

                    _logger.LogDebug("Mapped Keycloak roles to ClaimTypes.Role: {Roles}", string.Join(", ", roles));
                }
            }
            catch (JsonException)
            {
                // Fallback: treat the raw value as a single role string
                identity.AddClaim(new Claim(ClaimTypes.Role, rolesClaim.Value));
                _logger.LogDebug("Mapped single Keycloak role: {Role}", rolesClaim.Value);
            }

            return Task.FromResult(principal);
        }
    }
}
