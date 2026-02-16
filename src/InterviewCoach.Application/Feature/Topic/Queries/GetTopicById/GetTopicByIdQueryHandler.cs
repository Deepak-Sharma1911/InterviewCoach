using InterviewCoach.Application.Wrappers.ReadModels;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicById
{
    public class GetTopicByIdQueryHandler : IQueryHandler<GetTopicByIdQuery, TopicDetailsDto>
    {
        private readonly ILogger<GetTopicByIdQueryHandler> logger;
        private readonly ITopicReadRepository topic;
        private readonly IUnitOfWork unit;
        private readonly ICurrentUser user;
        private readonly ISystemClock systemClock;


        public GetTopicByIdQueryHandler(ILogger<GetTopicByIdQueryHandler> logger, ITopicReadRepository topic, IUnitOfWork unit, ICurrentUser user, ISystemClock systemClock)
        {
            this.logger = logger;
            this.topic = topic;
            this.unit = unit;
            this.user = user;
            this.systemClock = systemClock;
        }
        public async Task<TopicDetailsDto> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {QueryName} with TopicId: {TopicId}", nameof(GetTopicByIdQuery), request.TopicId);
            var topicEntity = await topic.GetByIdAsync(request.TechId, request.TopicId, cancellationToken);
            if (topicEntity is null)
            {
                logger.LogWarning("Topic with ID {TopicId} not found.", request.TopicId);
                return null;
            }
            logger.LogInformation("Successfully retrieved topic with ID {TopicId}.", request.TopicId);
            return topicEntity;
        }
    }
}
