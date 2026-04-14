using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Application
{
    /// <summary>
    /// Binds to the "Keycloak" section in appsettings.json.
    /// Inject via IOptions&lt;KeycloakOptions&gt;.
    /// </summary>
    public sealed class KeycloakOptions
    {
        public const string SectionName = "Keycloak";

        /// <summary>
        /// Keycloak realm base URL, e.g. http://localhost:8080/realms/your-realm
        /// The API uses this to auto-discover JWKS for token signature validation.
        /// </summary>
        public string Authority { get; set; } = string.Empty;

        /// <summary>
        /// The client ID registered in Keycloak. Used as the expected JWT audience.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Client ID used when proxying token requests to Keycloak.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Client secret. Store this in environment variables or secrets manager in production.
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// Set to true in production (requires valid HTTPS on Keycloak).
        /// Keep false for local development.
        /// </summary>
        public bool RequireHttpsMetadata { get; set; } = true;

        /// <summary>
        /// Full token endpoint URL, e.g.
        /// http://localhost:8080/realms/your-realm/protocol/openid-connect/token
        /// </summary>
        public string TokenUrl { get; set; } = string.Empty;
    }

}
