using InterviewCoach.Domain.Common;
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
        private Page() { }
        internal static Page Create(
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
                Id, type, title, content, displayOrder, modifiedBy, utcNow);

            _sections.Add(section);
            SetModified(modifiedBy, utcNow);
        }

        public void Publish(Guid modifiedBy, DateTime utcNow)
        {
            if (!_sections.Any())
                throw new DomainException("Cannot publish page without sections.");

            IsPublished = true;
            SetModified(modifiedBy, utcNow);
        }
        public static Page Rehydrate(
                                        Guid id,
                                        Guid topicId,
                                        string title,
                                        string slug,
                                        string? summary,
                                        bool isPublished,
                                        Guid createdBy,
                                        DateTime createdUtc,
                                        Guid? lastModifiedBy,
                                        DateTime? lastModifiedUtc,
                                        IEnumerable<PageSection> sections)
        {
            var page = new Page
            {
                Id = id,
                TopicId = topicId,
                Title = title,
                Slug = slug,
                Summary = summary,
                IsPublished = isPublished
            };

            page.SetCreated(createdBy, createdUtc);

            if (lastModifiedBy.HasValue && lastModifiedUtc.HasValue)
                page.SetModified(lastModifiedBy.Value, lastModifiedUtc.Value);

            foreach (var section in sections)
                page._sections.Add(section);

            return page;
        }

        public void UpdateSection(Guid sectionId, string title, string content, int displayOrder, Guid modifiedBy, DateTime utcNow)
        {
            if (IsPublished)
                throw new DomainException("Cannot modify a published page.");

            var section = _sections.FirstOrDefault(x => x.Id == sectionId);

            if (section is null)
                throw new DomainException("Section not found.");

            section.Update(title, content, displayOrder, modifiedBy, utcNow);

            SetModified(modifiedBy, utcNow);
        }

        public void RemoveSection(Guid sectionId, Guid modifiedBy, DateTime utcNow)
        {
            if (IsPublished)
                throw new DomainException("Cannot modify a published page.");

            var section = _sections.FirstOrDefault(x => x.Id == sectionId);

            if (section is null)
                throw new DomainException("Section not found.");

            _sections.Remove(section);

            SetModified(modifiedBy, utcNow);
        }

        public void RemovePage(Guid modifiedBy, DateTime utcNow)
        {
            if (IsPublished)
                throw new DomainException("Cannot remove a published page.");
            SetModified(modifiedBy, utcNow);
        }
    }
}

