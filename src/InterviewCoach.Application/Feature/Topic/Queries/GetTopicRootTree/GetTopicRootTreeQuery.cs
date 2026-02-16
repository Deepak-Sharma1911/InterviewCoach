using InterviewCoach.Application.Wrappers.ReadModels;

namespace InterviewCoach.Application.Feature.Topic.Queries.GetTopicRootTree
{
    public sealed record GetTopicRootTreeQuery(Guid TechId) : IQuery<IReadOnlyList<TopicTreeItem>>;
}
