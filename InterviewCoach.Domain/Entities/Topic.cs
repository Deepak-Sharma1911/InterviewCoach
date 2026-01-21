using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Topic : Entity<Guid>
    {
        private readonly List<Page> _pages = new();
        public Guid? ParentTopicId { get; private set; }
        public string Title { get; private set; } = null!;
        public string Slug { get; private set; } = null!;
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }
        public IReadOnlyCollection<Page> Pages => _pages.AsReadOnly();
        private Topic() { }
        public static Topic Create(
            string title,
            string slug,
            int displayOrder,
            Guid? parentTopicId,
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
                ParentTopicId = parentTopicId,
                IsActive = true
            };

            topic.SetCreated(createdBy, utcNow);
            return topic;
        }

        public Page AddPage(
            string title,
            string slug,
            string? summary,
            Guid createdBy,
            DateTime utcNow)
        {
            if (!IsActive)
                throw new DomainException("Cannot add page to inactive topic.");

            if (_pages.Any(p => p.Slug == slug))
                throw new DomainException("Duplicate page slug.");

            var page = Page.Create(Id, title, slug, summary, createdBy, utcNow);
            _pages.Add(page);

            SetModified(createdBy, utcNow);
            return page;
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



