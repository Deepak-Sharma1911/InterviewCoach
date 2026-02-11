using InterviewCoach.Application.Feature.Topic.ReadModels;


namespace InterviewCoach.Application.Abstractions
{
    public interface ITopicReadRepository
    {
        Task<IReadOnlyList<TopicTreeItem>> GetRootTreeAsync(CancellationToken ct);
        Task<TopicDetailsDto?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<TopicDomain.Topic?> GetTopicByIdAsync(Guid id, CancellationToken ct);
    }
}
