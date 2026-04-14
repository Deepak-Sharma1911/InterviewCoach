namespace InterviewCoach.API.Authorization
{
    /// <summary>
    /// Realm role names as configured in Keycloak.
    /// Use these constants everywhere instead of raw strings to avoid typos.
    /// </summary>
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }

    /// <summary>
    /// Named authorization policy identifiers.
    /// Registered in AuthorizationExtensions and applied via [Authorize(Policy = Policies.X)].
    /// </summary>
    public static class Policies
    {
        /// <summary>Requires the Admin role.</summary>
        public const string AdminOnly = "AdminOnly";

        /// <summary>Requires Manager OR Admin role.</summary>
        public const string ManagerOrAdmin = "ManagerOrAdmin";

        /// <summary>Any successfully authenticated user.</summary>
        public const string AuthenticatedUser = "AuthenticatedUser";

        /// <summary>
        /// Example of a custom requirement-based policy.
        /// Requires a "birthdate" claim in the JWT indicating the user is 18+.
        /// Add a Keycloak protocol mapper for "birthdate" to use this.
        /// </summary>
        public const string MinimumAge = "MinimumAge";
    }

}
