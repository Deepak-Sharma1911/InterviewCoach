using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Topic : Entity<Guid>
    {
        private readonly List<Topic> _children = new();

        public string Title { get; private set; } = null!;
        public string Slug { get; private set; } = null!;
        public Guid? ParentTopicId { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }

        public IReadOnlyCollection<Topic> Children => _children.AsReadOnly();

        private Topic() { } // EF rehydration

        public static Topic CreateRoot(
            string title,
            string slug,
            int displayOrder,
            Guid createdBy,
            DateTime utcNow)
        {
            Validate(title, slug);

            var topic = new Topic
            {
                Id = Guid.NewGuid(),
                Title = title,
                Slug = slug,
                DisplayOrder = displayOrder,
                IsActive = true
            };

            topic.SetCreated(createdBy, utcNow);
            return topic;
        }

        public static Topic CreateChild(
            Guid parentTopicId,
            string title,
            string slug,
            int displayOrder,
            Guid createdBy,
            DateTime utcNow)
        {
            Validate(title, slug);

            var topic = new Topic
            {
                Id = Guid.NewGuid(),
                ParentTopicId = parentTopicId,
                Title = title,
                Slug = slug,
                DisplayOrder = displayOrder,
                IsActive = true
            };

            topic.SetCreated(createdBy, utcNow);
            return topic;
        }

        public void Deactivate(Guid modifiedBy, DateTime utcNow)
        {
            if (!IsActive)
                return;

            IsActive = false;
            SetModified(modifiedBy, utcNow);
        }

        private static void Validate(string title, string slug)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Topic title is required.");

            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Topic slug is required.");
        }
    }

}

