using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Topic.ReadModels
{
    public sealed class PageSectionDto
    {
        public Guid Id { get; init; }
        public PageSectionType Type { get; init; }
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
        public int DisplayOrder { get; init; }
    }
}
