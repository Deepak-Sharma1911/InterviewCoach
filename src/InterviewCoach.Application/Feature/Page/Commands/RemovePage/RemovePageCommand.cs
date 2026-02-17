namespace InterviewCoach.Application.Feature.Page.Commands.RemovePage
{
    public sealed record RemovePageCommand(Guid PageId) : ICommand;
}
