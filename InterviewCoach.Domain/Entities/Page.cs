using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Entities;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Page : Entity<Guid>
    {
        private readonly List<PageSection> _sections = new();

        public Guid TopicId { get; private set; }
        public string Title { get; private set; } = null!;
        public string Slug { get; private set; } = null!;
        public string? Summary { get; private set; }
        public bool IsPublished { get; private set; }

        public IReadOnlyCollection<PageSection> Sections => _sections.AsReadOnly();

        private Page() { } // EF rehydration

        public static Page Create(
            Guid topicId,
            string title,
            string slug,
            string? summary,
            Guid createdBy,
            DateTime utcNow)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Page title is required.");

            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Page slug is required.");

            var page = new Page
            {
                Id = Guid.NewGuid(),
                TopicId = topicId,
                Title = title,
                Slug = slug,
                Summary = summary,
                IsPublished = false
            };

            page.SetCreated(createdBy, utcNow);
            return page;
        }

        public void AddSection(
            PageSectionType type,
            string title,
            string content,
            int displayOrder,
            Guid modifiedBy,
            DateTime utcNow)
        {
            if (IsPublished)
                throw new DomainException("Cannot modify a published page.");

            var section = PageSection.Create(
                Id,
                type,
                title,
                content,
                displayOrder,
                modifiedBy,
                utcNow);

            _sections.Add(section);
            SetModified(modifiedBy, utcNow);
        }

        public void Publish(Guid modifiedBy, DateTime utcNow)
        {
            if (IsPublished)
                throw new DomainException("Page is already published.");

            if (!_sections.Any())
                throw new DomainException("Cannot publish a page without sections.");

            IsPublished = true;
            SetModified(modifiedBy, utcNow);
        }
    }
}

