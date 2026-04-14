using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCoach.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        private ISender _sender;
        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
