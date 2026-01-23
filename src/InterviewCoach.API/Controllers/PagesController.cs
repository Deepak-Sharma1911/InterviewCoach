using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCoach.API.Controllers
{
    public class PagesController : BaseController
    {
        private readonly ILogger<PagesController> _logger;
        public PagesController(ILogger<PagesController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{id:guid}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            _logger.LogInformation("Publishing page ID: {PageId}", id);
            await Sender.Send(new PublishPageCommand(id));
            return NoContent();
        }

        [HttpPost("{id:guid}/sections")]
        public async Task<IActionResult> AddSection(Guid id, AddPageSectionRequest request)
        {
            _logger.LogInformation("Adding section to page ID: {PageId} with title: {Title}", id, request.Title);
            await Sender.Send(new AddPageSectionCommand(
                id,
                request.Type,
                request.Title,
                request.Content,
                request.DisplayOrder));

            return NoContent();
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            _logger.LogInformation("Getting page by slug: {Slug}", slug);
            var page = await Sender.Send(new GetPageBySlugQuery(slug));
            return Ok(page);
        }

    }
}
