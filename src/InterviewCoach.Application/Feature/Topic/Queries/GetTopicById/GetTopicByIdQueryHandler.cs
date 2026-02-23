using InterviewCoach.Application.Wrappers.ReadModels;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicById
{
    public class GetTopicByIdQueryHandler : IQueryHandler<GetTopicByIdQuery, TopicTreeItem>
    {
        private readonly ILogger<GetTopicByIdQueryHandler> _logger;
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unit;
        private readonly ICurrentUser _user;
        private readonly ISystemClock _systemClock;


        public GetTopicByIdQueryHandler(ILogger<GetTopicByIdQueryHandler> logger, ITopicRepository topicRepository, IUnitOfWork unit, ICurrentUser user, ISystemClock systemClock)
        {
            _logger = logger;
            _topicRepository = topicRepository;
            _unit = unit;
            _user = user;
            _systemClock = systemClock;
        }
        public async Task<TopicTreeItem> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {QueryName} with TopicId: {TopicId}", nameof(GetTopicByIdQuery), request.TopicId);
            var topicEntity = await _topicRepository.GetRootTreeByIdAsync(request.TopicId, cancellationToken);
            if (topicEntity is null)
            {
                _logger.LogWarning("Topic with ID {TopicId} not found.", request.TopicId);
                return null;
            }
            return topicEntity;
        }
    }
}
