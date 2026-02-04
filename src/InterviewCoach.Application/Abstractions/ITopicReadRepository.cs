namespace InterviewCoach.Application.Abstractions
{
    public interface ITopicReadRepository
    {
        Task<IReadOnlyList<TopicTreeNodeDto>> GetRootTreeAsync(CancellationToken ct);
        Task<TopicDto?> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
