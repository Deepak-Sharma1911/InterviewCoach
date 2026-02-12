using MediatR;

namespace InterviewCoach.Application.Feature.Page.Commands.UpdatePageSection
{
    public sealed record UpdatePageSectionCommand(Guid PageId, Guid SectionId,string Title,string Content,int DisplayOrder) : ICommand<Unit>;

}
