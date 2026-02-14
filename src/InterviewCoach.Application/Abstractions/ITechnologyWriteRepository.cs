using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    public interface ITechnologyWriteRepository
    {
        Task AddAsync(Technology technology, CancellationToken token);
        Task UpdateAsync(Technology technology, CancellationToken token);
    }
}
