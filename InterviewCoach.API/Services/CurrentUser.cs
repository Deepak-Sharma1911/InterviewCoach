using InterviewCoach.Application.Abstractions;
using System.Security.Claims;

namespace InterviewCoach.API.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                return userIdString != null ? Guid.Parse(userIdString.Value) : Guid.Empty;
            }
        }
    }
}
