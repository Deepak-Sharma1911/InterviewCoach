using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    internal class TechnologyReadRepository : ITechnologyReadRepository
    {
        private readonly ILogger<TechnologyReadRepository> _logger;
        private readonly ApplicationContext _context;
        public TechnologyReadRepository(ILogger<TechnologyReadRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;

        }
        public async Task<IReadOnlyList<Technology>> GetAllAsync(CancellationToken token)
        {
            var list = await _context.Technology
                                     .Where(x => x.IsActive)
                                     .OrderBy(x => x.Id)
                                     .AsNoTracking()
                                     .ToListAsync(token);

            return list.Select(TechnologyMapper.ToDomainTechnology).ToList();
        }

        public async Task<Technology> GetByIdAsync(Guid id, CancellationToken token)
        {
            var efEntity = await _context.Technology
                                         .AsNoTracking()
                                         .OrderBy(x => x.Id)
                                         .Where(x => x.IsActive)
                                         .FirstOrDefaultAsync(x => x.Id == id, token);

            return efEntity == null ? null : TechnologyMapper.ToDomainTechnology(efEntity);
        }

        public async  Task<Technology> GetBySlugAsync(string slug, CancellationToken token)
        {
            var efEntity = await _context.Technology
                                         .Where(x => x.IsActive)
                                         .OrderBy(x => x.Id)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(x => x.Slug == slug, token);

            return efEntity == null ? null : TechnologyMapper.ToDomainTechnology(efEntity);
        }
    }
}
