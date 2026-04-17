using InterviewCoach.Application;
using InterviewCoach.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace InterviewCoach.API.Controllers
{
    /// <summary>
    /// Handles token issuance and refresh by proxying requests to Keycloak.
    /// Use this from Swagger UI to get a token, then click Authorize and paste it in.
    /// </summary>
    [AllowAnonymous]
    [Produces("application/json")]
    [Tags("Authentication")]
    public class AuthController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly KeycloakOptions _options;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IHttpClientFactory httpClientFactory,
            IOptions<KeycloakOptions> options,
            ILogger<AuthController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// Exchange username and password for a JWT access token.
        /// Copy the returned accessToken, click Authorize at the top of Swagger, and paste it in.
        /// </summary>
        /// <remarks>
        /// Uses Keycloak's Resource Owner Password Credentials (ROPC) flow.
        /// This is convenient for development/testing. For production front-ends,
        /// prefer the Authorization Code + PKCE flow directly from the client.
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> Login([FromBody] TokenRequest request)
        {
            var formData = new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = _options.ClientId,
                ["username"] = request.Username,
                ["password"] = request.Password,
                ["scope"] = "openid profile email roles"
            };

            var result = await PostToKeycloakAsync(formData);
            return result;
        }

        /// <summary>
        /// Exchange a refresh token for a new access token without re-entering credentials.
        /// </summary>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var formData = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = _options.ClientId,
                ["client_secret"] = _options.ClientSecret,
                ["refresh_token"] = request.RefreshToken
            };

            var result = await PostToKeycloakAsync(formData);
            return result;
        }

        /// <summary>
        /// Returns the claims present in the current request's JWT token.
        /// Useful for debugging — confirms what roles and attributes Keycloak is sending.
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Me()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new { claims });
        }

        // ── Private helpers ──────────────────────────────────────────────────────

        private async Task<IActionResult> PostToKeycloakAsync(Dictionary<string, string> formData)
        {
            var client = _httpClientFactory.CreateClient("keycloak");

            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync(
                    _options.TokenUrl,
                    new FormUrlEncodedContent(formData));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Could not reach Keycloak at {Url}", _options.TokenUrl);
                return Problem(
                    title: "Keycloak Unreachable",
                    detail: "Could not connect to the authentication server. Make sure Keycloak is running.",
                    statusCode: StatusCodes.Status502BadGateway);
            }

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Keycloak token request failed. Status: {Status}, Body: {Body}",
                    response.StatusCode, content);

                return Problem(
                    title: "Authentication Failed",
                    detail: "Invalid credentials or expired token.",
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            var json = JsonSerializer.Deserialize<JsonElement>(content);

            var tokenResponse = new TokenResponse(
                AccessToken: json.GetProperty("access_token").GetString()!,
                RefreshToken: json.GetProperty("refresh_token").GetString()!,
                ExpiresIn: json.GetProperty("expires_in").GetInt32(),
                TokenType: json.GetProperty("token_type").GetString()!
            );

            return Ok(tokenResponse);
        }
    }

}
