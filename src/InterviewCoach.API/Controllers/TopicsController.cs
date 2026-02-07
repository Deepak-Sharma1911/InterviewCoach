using InterviewCoach.Application.Feature.Topic.Commands.CreateRootTopic;
using InterviewCoach.Application.Feature.Topic.Queries.GetTopicById;
using InterviewCoach.Application.Feature.Topic.Queries.GetTopicRootTree;
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

        /// <summary>
        /// Gets a topic by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id,CancellationToken token)
        {
            _logger.LogInformation("Getting topic by ID: {TopicId}", id);
            var topic = await Sender.Send(new GetTopicByIdQuery(id));
            return Ok(topic);
        }

        /// <summary>
        /// Gets all topics in a tree structure.
        /// </summary>
        /// <returns></returns>
        [HttpGet("tree")]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            _logger.LogInformation("Getting all topics in tree structure");
            var topics = await Sender.Send(new GetTopicRootTreeQuery());
            return Ok(topics);
        }

        /// <summary>
        /// Creates a new topic using the specified request data.
        /// </summary>
        /// <param name="request">The command containing the details of the topic to create. Cannot be null.</param>
        /// <returns>A response with status code 201 (Created) containing the created topic and a location header referencing the
        /// new resource.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateRootTopicCommand request,CancellationToken token)
        {
            _logger.LogInformation("Creating new topic with title: {Title}", request.Title);
            var result = await Sender.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        /// <summary>
        /// Adds a new page to the specified topic.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}/pages")]
        public async Task<IActionResult> AddPage(Guid id, AddPageRequest request,CancellationToken token)
        {
            _logger.LogInformation("Adding page to topic ID: {TopicId} with title: {Title}", id, request.Title);
            var pageId = await Sender.Send(new AddPageToTopicCommand(
                id,
                request.Title,
                request.Slug,
                request.Summary));

            return Ok(new { PageId = pageId });
        }

        /// <summary>
        ///  Deactivates the specified topic.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            _logger.LogInformation("Deactivating topic ID: {TopicId}", id);
            await Sender.Send(new DeactivateTopicCommand(id));
            return NoContent();
        }
    }
}
