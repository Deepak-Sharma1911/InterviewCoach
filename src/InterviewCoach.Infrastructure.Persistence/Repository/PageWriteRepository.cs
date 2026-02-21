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

        public async Task<Guid> AddAsync(Page page, CancellationToken token)
        {
            if (!await _context.Topics.AnyAsync(t => t.Id == page.TopicId, token))
                throw new InvalidOperationException("Topic not found.");
            var pageEntity = PageMapper.ToEntityPage(page);
            await _context.Pages.AddAsync(pageEntity, token);            
            await _context.SaveChangesAsync(token);
            return pageEntity.Id;
        }

        public async Task RemoveAsync(Page page, CancellationToken token)
        {
            var ef = await _context.Pages.Where(x => x.Id == page.Id).FirstOrDefaultAsync(token);
            if (ef == null)
                throw new InvalidOperationException("Page not found.");
            _context.Pages.Remove(ef);
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

            var domainSectionIds = page.Sections.Select(x => x.Id).ToHashSet();

            var removedSections = ef.PageSections
                .Where(x => !domainSectionIds.Contains(x.Id))
                .ToList();

            foreach (var removed in removedSections)
                _context.PageSections.Remove(removed);

            foreach (var section in page.Sections)
            {
                var efSection = ef.PageSections
                    .FirstOrDefault(x => x.Id == section.Id);

                if (efSection == null)
                {
                    ef.PageSections.Add(PageSectionMapper.ToEntityPageSection(section));
                }
                else
                {
                    efSection.Title = section.Title;
                    efSection.Content = section.Content;
                    efSection.DisplayOrder = section.DisplayOrder;
                    efSection.LastModifiedBy = section.LastModifiedBy;
                    efSection.LastUtcModified = section.LastUtcModified;
                }
            }
        }
    }
}
