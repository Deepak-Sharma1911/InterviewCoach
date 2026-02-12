namespace InterviewCoach.Application.Feature.Page.Commands.RemovePageSection
{
    public sealed record RemovePageSectionCommand(Guid PageId, Guid SectionId) : ICommand;

}
