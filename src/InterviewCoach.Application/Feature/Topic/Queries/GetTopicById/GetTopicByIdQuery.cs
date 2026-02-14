using InterviewCoach.Application.Wrappers.ReadModels;

namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicById
{
    public record GetTopicByIdQuery(Guid TopicId) : IQuery<TopicDetailsDto>;

}
