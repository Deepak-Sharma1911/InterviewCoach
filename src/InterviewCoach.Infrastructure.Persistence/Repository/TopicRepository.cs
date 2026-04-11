using InterviewCoach.Application.Wrappers.ReadModels;
using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ILogger<TopicRepository> _logger;
        private readonly ApplicationContext _context;
        public TopicRepository(ILogger<TopicRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<Topic?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Topics
                .FirstOrDefaultAsync(t => t.Id == id, token);
        }
        public async Task<TopicDomain.Topic?> GetByIdWithPagesAsync(Guid id, CancellationToken token)
        {
            return await _context.Topics.Include(t => t.Pages).FirstOrDefaultAsync(t => t.Id == id, token);
        }
        public async Task AddAsync(Topic topic, CancellationToken token)
        {
            await _context.Topics.AddAsync(topic, token);
        }
        public async Task<IReadOnlyList<TopicTreeItem>> GetRootTreeAsync(Guid techId, CancellationToken ct)
        {
            return await _context.Topics.AsNoTracking()
                                        .Where(t => t.ParentTopicId == null && t.IsActive && t.TechId == techId)
                                        .Select(x => new TopicTreeItem
                                        (
                                             x.Id,
                                             x.Title,
                                             x.Slug,
                                             new List<TopicTreeItem>(),
                                             x.Pages.Select(x => new PageLinkItem
                                             (
                                                x.Id,
                                                x.Title,
                                                x.Slug
                                                )).ToList()
                                        )).ToListAsync(ct);
        }
        public async Task<TopicTreeItem> GetRootTreeByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Topics.AsNoTracking()
                                         .Where(t => t.ParentTopicId == null && t.Id == id && t.IsActive)
                                         .Select(x => new TopicTreeItem
                                         (
                                              x.Id,
                                              x.Title,
                                              x.Slug,
                                              new List<TopicTreeItem>(),
                                              x.Pages.Select(x => new PageLinkItem
                                              (
                                                 x.Id,
                                                 x.Title,
                                                 x.Slug
                                                 )).ToList()
                                         )).FirstOrDefaultAsync(token);
        }
    }
}
