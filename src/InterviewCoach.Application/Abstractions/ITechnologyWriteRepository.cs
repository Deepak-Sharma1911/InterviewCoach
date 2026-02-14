using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Abstractions
{
    internal interface ITechnologyWriteRepository
    {
        Task AddAsync(Technology technology, CancellationToken token);
        Task UpdateAsync(Technology technology, CancellationToken token);
    }
}
