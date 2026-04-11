using InterviewCoach.Application.Wrappers.ReadModels;
using InterviewCoach.Domain.Entities;


namespace InterviewCoach.Application.Abstractions
{
    public interface ITopicRepository
    {
        Task<IReadOnlyList<TopicTreeItem>> GetRootTreeAsync(Guid techId,CancellationToken ct);
        Task<TopicTreeItem?> GetRootTreeByIdAsync(Guid id, CancellationToken token);
        Task<Topic?> GetByIdWithPagesAsync(Guid id, CancellationToken token);
        Task<Topic> GetByIdAsync(Guid id, CancellationToken token);
        Task AddAsync(Topic topic, CancellationToken token);
    }
}
