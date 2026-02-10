namespace InterviewCoach.Application.Feature.Topic.Commands.CreateChildTopic
{
    public record AddPageToTopicCommand(Guid ParentTopicId, string Title, string Slug, string? Summary) : ICommand<Guid>;

}
