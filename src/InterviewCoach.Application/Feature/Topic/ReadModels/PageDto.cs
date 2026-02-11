namespace InterviewCoach.Application.Feature.Topic.ReadModels
{
    public class PageDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = null!;
        public string Slug { get; init; } = null!;
        public bool IsPublished { get; init; }
    }
}
