using InterviewCoach.Application.Abstractions;
using InterviewCoach.Infrastructure.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    internal sealed class UnitOfWork : IUnitOfWork
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
