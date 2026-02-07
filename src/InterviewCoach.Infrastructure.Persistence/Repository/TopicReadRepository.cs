using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Logging;
using Topic = InterviewCoach.Infrastructure.Persistence.Database.Entities.Topic;

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
        public async Task<TopicDomain.Topic> GetByIdAsync(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Getting topic by id: {Id}", id);
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid topic id: {Id}", id);
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }
            Topic topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id, ct);
            return topic.ToDomainTopic();

        }

        public async Task<IReadOnlyList<TopicDomain.Topic>> GetRootTreeAsync(CancellationToken ct)
        {
            _logger.LogInformation("Getting topic root tree");
            var topicEntities = await _context.Topics
                                        .Where(t => t.ParentTopicId == null && t.IsActive == true)
                                        .OrderBy(t => t.DisplayOrder)
                                        .ToListAsync(ct);

            var domainTopics = topicEntities.Select(te => te.ToDomainTopic()).ToList();
            return domainTopics;
        }
    }
}
