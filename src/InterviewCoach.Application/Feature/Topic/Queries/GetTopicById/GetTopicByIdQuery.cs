namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicById
{
    public record GetTopicByIdQuery(Guid TopicId) : IQuery<TopicDomain.Topic>;

}
