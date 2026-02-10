using InterviewCoach.Application.Feature.Topic.Commands.CreateChildTopic;
using InterviewCoach.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Topic.Commands.CreateTopicPage
{
    public class AddPageToTopicCommandHandler : ICommandHandler<AddPageToTopicCommand, Guid>
    {
        private readonly ILogger<AddPageToTopicCommandHandler> _logger;
        private readonly ITopicRepository _topics;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUser _user;
        private readonly ISystemClock _clock;

        public AddPageToTopicCommandHandler(ILogger<AddPageToTopicCommandHandler> logger, ITopicRepository topics, IUnitOfWork uow, ICurrentUser user, ISystemClock clock)
        {
            _logger = logger;
            _topics = topics;
            _uow = uow;
            _user = user;
            _clock = clock;
        }

        public async Task<Guid> Handle(AddPageToTopicCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding page to topic {TopicId} with title {Title}", command.ParentTopicId, command.Title);
            var parentTopic = await _topics.GetByIdAsync(command.ParentTopicId, cancellationToken);
            if (parentTopic is null)
            {
                _logger.LogWarning("Parent topic with id {TopicId} not found", command.ParentTopicId);
                throw new NotFoundException(command.ParentTopicId);
            }
            var page = parentTopic.AddPage(command.Title, command.Slug, command.Summary, _user.UserId, _clock.UtcNow);
            await _uow.SaveChangesAsync(cancellationToken);
            return page.Id;
        }
    }
}