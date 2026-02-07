using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface ITopicRepository
    {
        Task AddAsync(Topic topic, CancellationToken ct);
    }
}
