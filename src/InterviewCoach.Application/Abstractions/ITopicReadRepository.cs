namespace InterviewCoach.Application.Abstractions
{
    public interface ITopicReadRepository
    {
        Task<IReadOnlyList<TopicDomain.Topic>> GetRootTreeAsync(CancellationToken ct);
        Task<TopicDomain.Topic> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
