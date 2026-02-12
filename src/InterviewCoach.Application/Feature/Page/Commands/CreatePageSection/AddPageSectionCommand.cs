using InterviewCoach.Domain.Entities;
using MediatR;

namespace InterviewCoach.Application.Feature.Page.Commands.CreatePageSection
{
    public sealed record AddPageSectionCommand(Guid PageId, PageSectionType Type, string Title, string Content, int DisplayOrder) : ICommand<Unit>;

}
