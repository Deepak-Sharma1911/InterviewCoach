namespace InterviewCoach.Application.Wrappers.ReadModels
{
    public sealed class TopicDetailsDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = null!;
        public string Slug { get; init; } = null!;
        public bool IsActive { get; init; }
        public IReadOnlyList<PageDto> Pages { get; init; } = [];
    }

}
