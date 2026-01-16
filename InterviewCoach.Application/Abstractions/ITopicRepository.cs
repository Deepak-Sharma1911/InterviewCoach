using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface ITopicRepository
    {

        Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);
        Task<Topic?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Topic topic, CancellationToken cancellationToken);
        Task<IReadOnlyList<Topic>> GetActiveTopicsAsync(CancellationToken cancellationToken);
    }
}
