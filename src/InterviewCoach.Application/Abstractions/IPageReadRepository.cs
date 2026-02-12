using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface IPageReadRepository
    {
        Task<Page?> GetByIdAsync(Guid id, CancellationToken token);
        Task<Page?> GetBySlugAsync(string slug, CancellationToken token);
    }
}
