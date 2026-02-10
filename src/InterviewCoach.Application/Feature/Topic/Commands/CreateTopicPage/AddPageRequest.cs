namespace InterviewCoach.Application.Feature.Topic.Commands.CreateTopicPage
{
    public sealed record AddPageRequest
    {
        public string Title { get; init; } = null!;
        public string Slug { get; init; } = null!;
        public string? Summary { get; init; }
    }
}
