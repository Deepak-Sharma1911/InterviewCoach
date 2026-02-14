using InterviewCoach.Application.Feature.Technology.Commands.CreateTechnology;
using InterviewCoach.Application.Feature.Technology.Commands.DeleteTechnology;
using InterviewCoach.Application.Feature.Technology.Queries.GetTechnologies;
using InterviewCoach.Application.Feature.Technology.Queries.GetTechnologyById;
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

        /// <summary>
        /// Get Navigation Tree for the technology
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            _logger.LogInformation("Getting navigation menu");
            var menu = await Sender.Send(new GetTechnologiesQuery());
            return Ok(menu);
        }

        /// <summary>
        /// Get the Technology Based on the guid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Getting technology {Id}", id);
            var result = await Sender.Send(new GetTechnologyByIdQuery(id), token);
            return Ok(result);
        }

        /// <summary>
        /// Create a technology
        /// </summary>
        /// <param name="command"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(
        CreateTechnologyCommand command,
        CancellationToken token)
        {
            _logger.LogInformation("Creating technology {Title}", command.Title);
            var id = await Sender.Send(command, token);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        /// <summary>
        /// Delete a technology based on guid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Deleting technology {Id}", id);
            await Sender.Send(new DeleteTechnologyCommand(id), token);
            return NoContent();
        }
    }
}
