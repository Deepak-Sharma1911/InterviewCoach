using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface ITechnologyRepository
    {
        Task AddAsync(Technology technology, CancellationToken token);
        Task<Technology?> GetByIdAsync(Guid id, CancellationToken token);
        Task<Technology?> GetBySlugAsync(string slug, CancellationToken token);
        Task<IReadOnlyList<Technology>> GetAllAsync(CancellationToken token);
    }
}
