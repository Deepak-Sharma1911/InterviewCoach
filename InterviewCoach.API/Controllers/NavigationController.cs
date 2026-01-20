using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCoach.API.Controllers
{
    public class NavigationController : BaseController
    {
        private readonly ILogger<NavigationController> _logger;
        public NavigationController(ILogger<NavigationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            _logger.LogInformation("Getting navigation menu");
            var menu = await Sender.Send(new GetNavigationMenuQuery());
            return Ok(menu);
        }
    }
}
