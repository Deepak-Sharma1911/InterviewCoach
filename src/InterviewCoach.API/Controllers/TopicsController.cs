using InterviewCoach.Application.Feature.Topic.CreateTopicRoot;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCoach.API.Controllers
{
    public sealed class TopicsController : BaseController
    {
        private readonly ILogger<TopicsController> _logger;
        public TopicsController(ILogger<TopicsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting topic by ID: {TopicId}", id);
            var topic = await Sender.Send(new GetTopicByIdQuery(id));
            return Ok(topic);
        }

        [HttpGet("tree")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all topics in tree structure");
            var topics = await Sender.Send(new GetAllTopicsQuery());
            return Ok(topics);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicCommand request)
        {
            _logger.LogInformation("Creating new topic with title: {Title}", request.Title);
            var result = await Sender.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("{id:guid}/pages")]
        public async Task<IActionResult> AddPage(Guid id, AddPageRequest request)
        {
            _logger.LogInformation("Adding page to topic ID: {TopicId} with title: {Title}", id, request.Title);
            var pageId = await Sender.Send(new AddPageToTopicCommand(
                id,
                request.Title,
                request.Slug,
                request.Summary));

            return Ok(new { PageId = pageId });
        }

        [HttpPost("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            _logger.LogInformation("Deactivating topic ID: {TopicId}", id);
            await Sender.Send(new DeactivateTopicCommand(id));
            return NoContent();
        }
    }
}
