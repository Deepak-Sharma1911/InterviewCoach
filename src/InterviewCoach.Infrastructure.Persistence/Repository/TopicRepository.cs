using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public sealed class TopicRepository : ITopicRepository
    {
        private readonly ILogger<TopicRepository> _logger;
        private readonly ApplicationContext _context;
        public TopicRepository(ILogger<TopicRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task AddAsync(Topic topic, CancellationToken ct)
        {
            var entityTopic = topic.ToEntityTopic();
            await _context.Topics.AddAsync(entityTopic, ct);
        }
        public async Task<Topic> GetByIdAsync(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Getting topic by id: {Id}", id);
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid topic id: {Id}", id);
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }
            var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id, ct);
            return topic.ToDomainTopic();
        }

        public async Task UpdateAsync(Topic topic, CancellationToken token)
        {
            var ef = await _context.Topics
                .FirstOrDefaultAsync(x => x.Id == topic.Id, token);
            if (ef == null)
                throw new InvalidOperationException("Topic not found in DB.");
            ef.IsActive = !topic.IsActive;
        }
    }

}
