using InterviewCoach.Domain.Entities;
using MediatR;

namespace InterviewCoach.Application.Feature.Page.Commands.CreatePageSection
{
    public sealed record AddPageSectionCommand(Guid PageId, PageSectionTypeEnum Type, string Title, string Content, int DisplayOrder) : ICommand<Unit>;

}
