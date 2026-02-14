using InterviewCoach.Application.Feature.Topic.ReadModels;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public class TopicReadRepository : ITopicReadRepository
    {
        private readonly ILogger<TopicReadRepository> _logger;
        private readonly ApplicationContext _context;
        public TopicReadRepository(ILogger<TopicReadRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<TopicDetailsDto> GetByIdAsync(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Getting topic by id: {Id}", id);
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid topic id: {Id}", id);
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }
            return await _context.Topics
                                 .Where(t => t.Id == id)
                                 .Select(t => new TopicDetailsDto
                                 {
                                     Id = t.Id,
                                     Title = t.Title,
                                     Slug = t.Slug,
                                     IsActive = t.IsActive,
                                     Pages = t.Pages
                                         .Select(p => new PageDto
                                         {
                                             Id = p.Id,
                                             Title = p.Title,
                                             Slug = p.Slug,
                                             IsPublished = p.IsPublished
                                         })
                                         .ToList()
                                 }).FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyList<TopicTreeItem>> GetRootTreeAsync(CancellationToken ct)
        {
            _logger.LogInformation("Getting topic root tree");
            return await _context.Topics
                                 .Where(t => t.ParentTopicId == null && t.IsActive == true)
                                 .OrderBy(t => t.DisplayOrder)
                                 .Select(t => new TopicTreeItem(
                                     t.Id,
                                     t.Title,
                                     t.Slug,
                                     t.InverseParentTopic.Select(c =>
                                         new TopicTreeItem(
                                             c.Id,
                                             c.Title,
                                             c.Slug,
                                             new List<TopicTreeItem>(),
                                             new List<PageLinkItem>()
                                         )).ToList(),
                                     t.Pages.Select(p =>
                                         new PageLinkItem(p.Id, p.Title, p.Slug)).ToList()
                                 )).ToListAsync(ct);
        }

        public async Task<TopicDomain.Topic> GetTopicByIdAsync(Guid id, CancellationToken token)
        {
            Topic record = await _context.Topics
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, token);
            if (record == null)
                return null;
            return TopicMapper.ToDomainTopic(record);
        }
    }
}
