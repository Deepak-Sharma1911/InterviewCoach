using InterviewCoach.Domain.Entities;
using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Mappings;


namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public sealed class TopicRepository : ITopicRepository
    {
        private readonly ApplicationContext _context;
        public TopicRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Topic topic, CancellationToken ct)
        {
            var entityTopic = topic.ToEntityTopic();
            await _context.Topics.AddAsync(entityTopic, ct);
        }
    }

}
