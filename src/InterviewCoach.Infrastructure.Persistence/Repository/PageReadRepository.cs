using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public sealed class PageReadRepository : IPageReadRepository
    {
        private readonly ILogger<PageReadRepository> _logger;
        private readonly ApplicationContext _context;
        public PageReadRepository(ILogger<PageReadRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<Page> GetByIdAsync(Guid id, CancellationToken token)
        {
            var ef = await _context.Pages
                                   .Include(p => p.PageSections)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(x => x.Id == id, token);
            return ef is null ? null : PageMapper.ToDomainPage(ef);
        }

        public async Task<Page> GetBySlugAsync(string slug, CancellationToken token)
        {
            var ef = await _context.Pages
                 .Include(p => p.PageSections)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Slug == slug, token);
            return ef is null ? null : PageMapper.ToDomainPage(ef);
        }
    }

}
