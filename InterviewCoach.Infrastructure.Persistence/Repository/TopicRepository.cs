using InterviewCoach.Application.Abstractions;
using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    internal sealed class TopicRepository : ITopicRepository
    {
        private readonly ApplicationContext _context;
        public TopicRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> SlugExistsAsync(string slug, CancellationToken ct)
            => await _context.Topics.AnyAsync(t => t.Slug == slug, ct);

        public async Task<Topic?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var entityTopic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id, ct);
            return entityTopic?.ToDomainTopic();
        }
        public async Task AddAsync(Topic topic, CancellationToken ct)
        {
            var entityTopic = topic.ToEntityTopic();
            await _context.Topics.AddAsync(entityTopic, ct);
        }
        public async Task<IReadOnlyList<Topic>> GetActiveTopicsAsync(CancellationToken ct)
        {
            var topicEntity = await _context.Topics
                                            .Where(t => t.IsActive == true)
                                            .OrderBy(t => t.DisplayOrder)
                                            .ToListAsync(ct);

            return topicEntity.Select(te => te.ToDomainTopic()).ToList();
        }
    }

}
