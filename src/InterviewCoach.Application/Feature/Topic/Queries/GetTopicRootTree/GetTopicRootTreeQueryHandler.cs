using InterviewCoach.Application.Wrappers.ReadModels;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicRootTree
{
    public class GetTopicRootTreeQueryHandler : IQueryHandler<GetTopicRootTreeQuery, IReadOnlyList<TopicTreeItem>>
    {
        private readonly ILogger<GetTopicRootTreeQueryHandler> _logger;
        private readonly ITopicRepository _topicRepository;
        public GetTopicRootTreeQueryHandler(ILogger<GetTopicRootTreeQueryHandler> logger, ITopicRepository topicRepository)
        {
            _logger = logger;
            _topicRepository = topicRepository;
        }
        public async Task<IReadOnlyList<TopicTreeItem>> Handle(GetTopicRootTreeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get All the Topics");
            return await _topicRepository.GetRootTreeAsync(request.TechId,cancellationToken);
        }
    }
}
