using InterviewCoach.Application.Wrappers.ReadModels;

namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicById
{
    public record GetTopicByIdQuery(Guid TechId,Guid TopicId) : IQuery<TopicDetailsDto>;

}
