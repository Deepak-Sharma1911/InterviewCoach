using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public class TechnologyWriteRepository : ITechnologyWriteRepository
    {
        private readonly ILogger<TechnologyWriteRepository> _logger;
        private readonly ApplicationContext _context;
        public TechnologyWriteRepository(ILogger<TechnologyWriteRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;

        }
        public async Task AddAsync(Technology technology, CancellationToken token)
        {
            _logger.LogInformation("Adding Technology to database");
            var efEntity = TechnologyMapper.ToEntityTechnology(technology);
            await _context.Technologies.AddAsync(efEntity, token);
        }

        public async Task UpdateAsync(Technology technology, CancellationToken token)
        {
            _logger.LogInformation("Updating Technology");
            var existing = await _context.Technologies
               .FirstAsync(x => x.Id == technology.Id, token);

            existing.Title = technology.Title;
            existing.Slug = technology.Slug;
            existing.DisplayOrder = technology.DisplayOrder;
            existing.IsActive = technology.IsActive;
            existing.LastModifiedBy = technology.LastModifiedBy;
            existing.LastUtcModified = technology.LastUtcModified;
        }
    }
}
