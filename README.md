# Keycloak Auth Setup — .NET 8 Web API

Clean authentication and authorization infrastructure with no demo endpoints.
Drop your own controllers in and apply the patterns from `_AuthPatterns.cs`.

---

## Project Structure

```
KeycloakAuth/
├── docker-compose.yml
├── .env                                        ← Keycloak/DB credentials
├── keycloak/
│   └── realm-config/
│       └── your-realm.json                     ← Auto-imported on first start
└── src/KeycloakAuth.API/
    ├── Authorization/
    │   ├── PolicyConstants.cs                  ← Roles & Policies string constants
    │   ├── KeycloakRolesClaimsTransformer.cs   ← Maps "roles" claim → ClaimTypes.Role
    │   └── MinimumAgeRequirement.cs            ← Custom policy requirement + handler
    ├── Configuration/
    │   └── KeycloakOptions.cs                  ← Strongly-typed appsettings binding
    ├── Extensions/
    │   ├── AuthenticationExtensions.cs         ← JWT Bearer setup
    │   ├── AuthorizationExtensions.cs          ← Policy registration
    │   └── SwaggerExtensions.cs                ← Swagger + JWT UI
    ├── Controllers/
    │   └── _AuthPatterns.cs                    ← Copy-paste reference (delete when done)
    ├── Program.cs
    └── appsettings.json
```

---

## Step 1 — Start Keycloak

```bash
docker-compose up -d
```

- Keycloak: **http://localhost:8080**
- Admin console: **http://localhost:8080/admin** → `admin` / `admin`
- The realm `your-realm` is imported automatically with roles: `Admin`, `Manager`, `User`

> **After first start:** go to Keycloak admin → Clients → `your-api-client` → Credentials
> and replace the client secret in both Keycloak and `appsettings.json`.

---

## Step 2 — Configure appsettings.json

```json
"Keycloak": {
  "Authority":             "http://localhost:8080/realms/your-realm",
  "Audience":              "your-api-client",
  "ClientId":              "your-api-client",
  "ClientSecret":          "REPLACE_WITH_A_STRONG_SECRET",
  "RequireHttpsMetadata":  false,
  "TokenUrl":              "http://localhost:8080/realms/your-realm/protocol/openid-connect/token"
}
```

---

## Step 3 — Run the API

```bash
cd src/KeycloakAuth.API
dotnet run
```

---

## How to Apply Auth to Your Controllers

See `Controllers/_AuthPatterns.cs` for all patterns. Summary:

```csharp
// Whole controller requires auth
[Authorize]

// Single role
[Authorize(Roles = Roles.Admin)]

// Multiple roles (OR)
[Authorize(Roles = $"{Roles.Manager},{Roles.Admin}")]

// Named policy (role-based)
[Authorize(Policy = Policies.ManagerOrAdmin)]

// Named policy (custom requirement)
[Authorize(Policy = Policies.MinimumAge)]

// Public inside an [Authorize] controller
[AllowAnonymous]
```

---

## Adding a New Custom Policy

1. Add a constant to `Policies` in `PolicyConstants.cs`
2. Create a class implementing `IAuthorizationRequirement`
3. Create a class extending `AuthorizationHandler<YourRequirement>`
4. Register the policy in `AuthorizationExtensions.cs`
5. Register the handler: `services.AddSingleton<IAuthorizationHandler, YourHandler>()`

---

## Docker Cheatsheet

```bash
docker-compose up -d          # Start in background (auto-restarts on reboot)
docker-compose down           # Stop (data preserved in volume)
docker-compose down -v        # Stop + wipe all data
docker-compose logs -f        # Follow logs
```

## Features to be Added

1. Rolebased Access
2. UI Connectivity
3. Result pattern
4. Pagination
5. Lazy & Eager Loding Concept.
6. Content-netotiation.
7. Gzip/Brocaoli compression.
8. Unit Test and Integration Tests.
9. CI/CD Setup using Github Action/Azure Devops.
10. Caching
11. Rate Limiting 
12. Versioning
