namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicRootTree
{
    public class GetTopicRootTreeQueryHandler : IQueryHandler<GetTopicRootTreeQuery, IReadOnlyList<TopicDomain.Topic>>
    {
        private readonly ITopicReadRepository _repo;
        public GetTopicRootTreeQueryHandler(ITopicReadRepository repo)
        {
            _repo = repo;
        }
        public Task<IReadOnlyList<TopicDomain.Topic>> Handle(GetTopicRootTreeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
