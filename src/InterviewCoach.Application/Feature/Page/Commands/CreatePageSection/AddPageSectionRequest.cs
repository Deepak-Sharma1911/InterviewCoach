using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Page.Commands.CreatePageSection
{
    public record AddPageSectionRequest(PageSectionTypeEnum Type, string Title, string Content, int DisplayOrder);
}
