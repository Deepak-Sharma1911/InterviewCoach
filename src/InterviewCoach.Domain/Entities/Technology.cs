using InterviewCoach.Domain.Common;

namespace InterviewCoach.Domain.Entities
{
    public  class Technology : Entity<Guid>
    {
        private readonly List<Topic> _topics = new();
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }
        public byte[] RowVersion { get; private set; }
        public IReadOnlyCollection<Topic> Topics => _topics;
        private Technology() { }
        public Technology(string title, string slug, int displayOrder, Guid userId, DateTime utcNow)
        {
            Id = Guid.NewGuid();
            Title = title;
            Slug = slug;
            DisplayOrder = displayOrder;
            IsActive = true;
            CreatedBy = userId;
            CreatedUtcDate = utcNow;
            LastUtcModified = utcNow;
        }
        public static Technology Create(string title, string slug, int displayOrder, Guid userId, DateTime utcNow)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug is required.");

            return new Technology
            {
                Id = Guid.NewGuid(),
                Title = title,
                Slug = slug,
                DisplayOrder = displayOrder,
                IsActive = true,
                CreatedBy = userId,
                CreatedUtcDate = utcNow,
                LastUtcModified = utcNow
            };
        }
        public void Update(string title, string slug, int displayOrder, Guid userId, DateTime utcNow)
        {
            Title = title;
            Slug = slug;
            DisplayOrder = displayOrder;
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
        }
        public Topic AddTopic(string title, string slug, Guid? parentTopicId, int displayOrder, Guid userId, DateTime utcNow)
        {
            if (_topics.Any(t => t.Slug == slug))
                throw new InvalidOperationException("Duplicate topic slug.");

            if (parentTopicId.HasValue && !_topics.Any(t => t.Id == parentTopicId))
                throw new InvalidOperationException("Parent topic not found.");

            var topic = new Topic(Id, title, slug, parentTopicId, displayOrder, userId, utcNow);
            _topics.Add(topic);
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
            return topic;
        }
        public void Rename(string title, Guid userId, DateTime utcNow)
        {
            Title = title;
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
        }
        public void Deactivate(Guid userId, DateTime utcNow)
        {
            IsActive = false;
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
        }
    }
}