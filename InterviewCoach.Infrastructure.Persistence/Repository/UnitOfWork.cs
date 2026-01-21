using InterviewCoach.Application.Abstractions;
using InterviewCoach.Infrastructure.Persistence.Database;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
            => _context.SaveChangesAsync(ct);
    }
}
