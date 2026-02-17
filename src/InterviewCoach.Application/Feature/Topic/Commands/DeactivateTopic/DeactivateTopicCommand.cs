namespace InterviewCoach.Application.Feature.Topic.Commands.DeactivateTopic
{
    public sealed record DeactivateTopicCommand(Guid TopicId) : ICommand;
}
