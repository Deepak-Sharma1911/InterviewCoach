using InterviewCoach.Application.Wrappers.ReadModels;


namespace InterviewCoach.Application.Abstractions
{
    public interface ITopicReadRepository
    {
        Task<IReadOnlyList<TopicTreeItem>> GetRootTreeAsync(Guid technologyId ,CancellationToken ct);
        Task<TopicDetailsDto?> GetByIdAsync(Guid technologyId ,Guid id, CancellationToken ct);
        Task<TopicDomain.Topic?> GetTopicByIdAsync(Guid technologyId, Guid id, CancellationToken ct);
        Task<TopicDomain.Topic?> GetTopicByIdAsync( Guid id, CancellationToken ct);
    }
}
