using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly ILogger<TechnologyRepository> _logger;
        private readonly ApplicationContext _context;
        public TechnologyRepository(ILogger<TechnologyRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;

        }
        public async Task<IReadOnlyList<Technology>> GetAllAsync(CancellationToken token)
        {
           return await _context.Technology
                                     .Where(x => x.IsActive)
                                     .OrderBy(x => x.Id)
                                     .AsNoTracking()
                                     .ToListAsync(token);

        }
        public async Task<Technology> GetByIdAsync(Guid id, CancellationToken token)
        {
            var efEntity = await _context.Technology
                                         .OrderBy(x => x.Id)
                                         .Where(x => x.IsActive)
                                         .FirstOrDefaultAsync(x => x.Id == id, token);

            return efEntity == null ? null : efEntity;
        }
        public async Task<Technology> GetBySlugAsync(string slug, CancellationToken token)
        {
            var efEntity = await _context.Technology
                                         .Where(x => x.IsActive)
                                         .OrderBy(x => x.Id)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(x => x.Slug == slug, token);

            return efEntity == null ? null : efEntity;
        }
        public async Task AddAsync(Technology technology, CancellationToken token)
        {
            _logger.LogInformation("Adding Technology to database");
            await _context.Technology.AddAsync(technology, token);
        }
    }
}
