using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Wrappers.ReadModels
{
    public sealed class PageSectionDto
    {
        public Guid Id { get; init; }
        public PageSectionTypeEnum Type { get; init; }
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
        public int DisplayOrder { get; init; }
    }
}
