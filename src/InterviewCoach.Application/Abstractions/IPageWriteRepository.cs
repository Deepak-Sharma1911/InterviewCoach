using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface IPageWriteRepository
    {
        Task<Guid> AddAsync(Page page, CancellationToken token);
        Task UpdateAsync(Page page, CancellationToken token);

        Task RemoveAsync(Page page, CancellationToken token);
    }
}
