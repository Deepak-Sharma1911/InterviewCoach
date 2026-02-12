using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    internal class PageWriteRepository : IPageWriteRepository
    {
        private readonly ILogger<PageWriteRepository> _logger;
        private readonly ApplicationContext _context;
        public PageWriteRepository(ILogger<PageWriteRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task UpdateAsync(Page page, CancellationToken token)
        {
            var ef = await _context.Pages
                .Include(p => p.PageSections)
                .FirstOrDefaultAsync(x => x.Id == page.Id, token) ?? throw new InvalidOperationException("Page not found.");

            ef.Title = page.Title;
            ef.Slug = page.Slug;
            ef.Summary = page.Summary;
            ef.IsPublished = page.IsPublished;
            ef.LastModifiedBy = page.LastModifiedBy;
            ef.LastUtcModified = page.LastUtcModified;

            foreach (var section in page.Sections)
            {
                if (!ef.PageSections.Any(x => x.Id == section.Id))
                {
                    ef.PageSections.Add(PageSectionMapper.ToEntityPageSection(section));
                }
            }
        }
    }
}
