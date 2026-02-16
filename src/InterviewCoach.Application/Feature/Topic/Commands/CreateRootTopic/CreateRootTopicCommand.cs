namespace InterviewCoach.Application.Feature.Topic.Commands.CreateRootTopic
{
    public sealed record CreateRootTopicCommand(string Title, string Slug, int DisplayOrder, Guid ?ParentTopicId,Guid TechnologyId) : ICommand<Guid>;
}
