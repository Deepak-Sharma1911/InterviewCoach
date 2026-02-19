using InterviewCoach.Application.Feature.Page.Commands.CreatePageSection;
using InterviewCoach.Application.Feature.Page.Commands.PublishPage;
using InterviewCoach.Application.Feature.Page.Commands.RemovePage;
using InterviewCoach.Application.Feature.Page.Commands.RemovePageSection;
using InterviewCoach.Application.Feature.Page.Commands.UpdatePageSection;
using InterviewCoach.Application.Feature.Page.Queries.GetPageById;
using InterviewCoach.Application.Feature.Page.Queries.GetPageBySlug;
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

        /// <summary>
        /// Publishes a page by its unique identifier, making it available for users to view. 
        /// This action typically involves changing the page's status to "published" and may trigger additional processes such as notifications or cache updates.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}/publish")]
        public async Task<IActionResult> Publish(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Publishing page ID: {PageId}", id);
            await Sender.Send(new PublishPageCommand(id), token);
            return NoContent();
        }

        /// <summary>
        /// Adds a new section to an existing page, allowing for the expansion of content and structure.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}/sections")]
        public async Task<IActionResult> AddSection(Guid id, AddPageSectionCommand request, CancellationToken token)
        {
            _logger.LogInformation("Adding section to page ID: {PageId} with title: {Title}", id, request.Title);
            await Sender.Send(request, token);

            return NoContent();
        }

        /// <summary>
        /// Gets a page by its slug, which is a URL-friendly identifier typically derived from the page title.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug, CancellationToken token)
        {
            _logger.LogInformation("Getting page by slug: {Slug}", slug);
            var page = await Sender.Send(new GetPageBySlugQuery(slug), token);
            return Ok(page);
        }

        /// <summary>
        /// Gets a page by its slug, which is a URL-friendly identifier typically derived from the page title.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{PageId:guid}")]
        public async Task<IActionResult> GetById(Guid PageId, CancellationToken token)
        {
            _logger.LogInformation("Getting page by Guid: {Guid}", PageId);
            var page = await Sender.Send(new GetByPageIdQuery(PageId), token);
            return Ok(page);
        }

        /// <summary>
        /// Delete the Page by PageId
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{pageId:guid}")]
        public async Task<IActionResult> RemovePage(Guid pageId, CancellationToken token)
        {
            _logger.LogInformation("Removing  page ID: {PageId}", pageId);
            await Sender.Send(new RemovePageCommand(pageId), token);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing section of a page, allowing for modifications to the section's title, content, and display order.
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="sectionId"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{pageId:guid}/sections/{sectionId:guid}")]
        public async Task<IActionResult> UpdateSection(Guid pageId, Guid sectionId, UpdatePageSectionCommand request, CancellationToken token)
        {
            _logger.LogInformation("Updating section ID: {SectionId} of page ID: {PageId} with title: {Title}", sectionId, pageId, request.Title);
            await Sender.Send(request, token);

            return NoContent();
        }

        /// <summary>
        /// Removes a section from a page, allowing for the deletion of content that is no longer relevant or needed.
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="sectionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{pageId:guid}/sections/{sectionId:guid}")]
        public async Task<IActionResult> RemoveSection(Guid pageId, Guid sectionId, CancellationToken token)
        {
            _logger.LogInformation("Removing section ID: {SectionId} from page ID: {PageId}", sectionId, pageId);
            await Sender.Send(new RemovePageSectionCommand(pageId, sectionId), token);
            return NoContent();
        }


    }
}
