using InterviewCoach.Application.Feature.Page.Commands.CreatePageSection;
using InterviewCoach.Application.Feature.Page.Commands.PublishPage;
using InterviewCoach.Application.Feature.Page.Commands.RemovePageSection;
using InterviewCoach.Application.Feature.Page.Commands.UpdatePageSection;
using InterviewCoach.Application.Feature.Page.Queries.GetPageBySlug;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

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
        public async Task<IActionResult> Publish(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Publishing page ID: {PageId}", id);
            await Sender.Send(new PublishPageCommand(id), token);
            return NoContent();
        }

        [HttpPost("{id:guid}/sections")]
        public async Task<IActionResult> AddSection(Guid id, AddPageSectionCommand request, CancellationToken token)
        {
            _logger.LogInformation("Adding section to page ID: {PageId} with title: {Title}", id, request.Title);
            await Sender.Send(request, token);

            return NoContent();
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug, CancellationToken token)
        {
            _logger.LogInformation("Getting page by slug: {Slug}", slug);
            var page = await Sender.Send(new GetPageBySlugQuery(slug), token);
            return Ok(page);
        }

        [HttpPut("{pageId:guid}/sections/{sectionId:guid}")]
        public async Task<IActionResult> UpdateSection(Guid pageId, Guid sectionId, UpdatePageSectionCommand request, CancellationToken token)
        {
            _logger.LogInformation("Updating section ID: {SectionId} of page ID: {PageId} with title: {Title}", sectionId, pageId, request.Title);
            await Sender.Send(request, token);

            return NoContent();
        }

        [HttpDelete("{pageId:guid}/sections/{sectionId:guid}")]
        public async Task<IActionResult> RemoveSection(Guid pageId, Guid sectionId, CancellationToken token)
        {
            _logger.LogInformation("Removing section ID: {SectionId} from page ID: {PageId}", sectionId, pageId);
            await Sender.Send(new RemovePageSectionCommand(pageId, sectionId), token);
            return NoContent();
        }


    }
}
