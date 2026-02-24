using InterviewCoach.Domain.Entities;
using InterviewCoach.Domain.Exceptions;
using InterviewCoach.Infrastructure.Persistence.Database;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    internal class PageRepository : IPageRepository
    {
        private readonly ILogger<PageRepository> _logger;
        private readonly ApplicationContext _context;
        public PageRepository(ILogger<PageRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task AddAsync(Page page, CancellationToken token)
        {
            await _context.Pages.AddAsync(page, token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            Page page = await _context.Pages.FirstAsync(p => p.Id == id) ?? throw new NotFoundException(id);
            _context.Pages.Remove(page);
        }
        public async Task<Page> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Pages.FirstOrDefaultAsync(p => p.Id == id, token);
        }
        public async Task<Page> GetByIdWithSectionsAsync(Guid id, CancellationToken token)
        {
            return await _context.Pages.Include(p => p.Sections).FirstOrDefaultAsync(p => p.Id == id, token);
        }
        public async Task<Page> GetBySlugAsync(string slug, CancellationToken token)
        {
            return await _context.Pages.Include(x=>x.Sections).FirstOrDefaultAsync(p => p.Slug == slug, token);
        }
    }
}
