using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface IPageRepository
    {
        Task<Page?> GetByIdAsync(Guid id, CancellationToken token);
        Task<Page?> GetByIdWithSectionsAsync(Guid id, CancellationToken token);
        Task AddAsync(Page page, CancellationToken token);
        Task<Page?> GetBySlugAsync(string slug, CancellationToken token);
        Task DeleteAsync(Guid id, CancellationToken token);
    }
}
