namespace InterviewCoach.Application.Feature.Topic.Commands.DeactivateTopic
{
    public sealed record DeactivateTopicCommand(Guid TechnologyId,Guid TopicId) : ICommand;
}
