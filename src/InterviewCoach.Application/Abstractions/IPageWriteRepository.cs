using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface IPageWriteRepository
    {
        Task UpdateAsync(Page page, CancellationToken token);
    }
}
