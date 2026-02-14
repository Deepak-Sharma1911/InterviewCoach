using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface ITechnologyReadRepository
    {
        Task<Technology?> GetByIdAsync(Guid id, CancellationToken token);
        Task<Technology?> GetBySlugAsync(string slug, CancellationToken token);
        Task<IReadOnlyList<Technology>> GetAllAsync(CancellationToken token);
    }
}
