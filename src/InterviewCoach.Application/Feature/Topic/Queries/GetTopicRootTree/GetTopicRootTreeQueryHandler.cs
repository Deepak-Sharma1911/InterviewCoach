using InterviewCoach.Application.Feature.Topic.Queries.GetTopicById;
using InterviewCoach.Application.Wrappers.ReadModels;
using InterviewCoach.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicRootTree
{
    public class GetTopicRootTreeQueryHandler : IQueryHandler<GetTopicRootTreeQuery, IReadOnlyList<TopicTreeItem>>
    {
        private readonly ILogger<GetTopicRootTreeQueryHandler> _logger;
        private readonly ITopicReadRepository _topicRead;
        public GetTopicRootTreeQueryHandler(ILogger<GetTopicRootTreeQueryHandler> logger, ITopicReadRepository topicRead)
        {
            _logger = logger;
            _topicRead = topicRead;
        }
        public async Task<IReadOnlyList<TopicTreeItem>> Handle(GetTopicRootTreeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get All the Topics");
            return await _topicRead.GetRootTreeAsync(request.TechId, cancellationToken);
        }
    }
}
