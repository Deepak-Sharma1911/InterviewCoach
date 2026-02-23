using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Entities;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Page : Entity<Guid>
    {
        private readonly List<PageSection> _sections = new();
        public Guid TopicId { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Summary { get; private set; }
        public bool IsPublished { get; private set; }
        public bool IsActive { get; private set; }
        public byte[] RowVersion { get; private set; }
        public IReadOnlyCollection<PageSection> Sections => _sections;
        private Page() { }
        public Page(Guid topicId, string title, string slug, string summary, Guid userId, DateTime utcNow)
        {
            Id = Guid.NewGuid();
            TopicId = topicId;
            Title = title;
            Slug = slug;
            Summary = summary;
            IsPublished = false;
            CreatedBy = userId;
            CreatedUtcDate = utcNow;
            LastUtcModified = utcNow;
        }
        public void AddSection(PageSectionType sectionType, string title, string content, int displayOrder, Guid userId, DateTime utcNow)
        {
            if (_sections.Any(s => s.DisplayOrder == displayOrder))
                throw new InvalidOperationException("Duplicate section order.");

            var section = PageSection.Create(
                Id,
                sectionType,
                title,
                content,
                displayOrder,
                userId,
                utcNow);
            _sections.Add(section);
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
        }
        public static Page Create(Guid topicId, string title, string slug, string summary, Guid userId, DateTime utcNow)
        {
            return new Page
            {
                Id = Guid.NewGuid(),
                TopicId = topicId,
                Title = title,
                Slug = slug,
                Summary = summary,
                IsPublished = false,
                CreatedBy = userId,
                CreatedUtcDate = utcNow,
                LastUtcModified = utcNow
            };
        }
        public void Update(string title, string slug, string summary, Guid userId, DateTime utcNow)
        {
            Title = title;
            Slug = slug;
            Summary = summary;
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
        }
        public void Rename(string title, Guid userId, DateTime utcNow)
        {
            Title = title;
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
        }
        public void Publish(Guid userId, DateTime utcNow)
        {
            if (!_sections.Any())
                throw new InvalidOperationException("Cannot publish empty page.");
            IsPublished = true;
            LastModifiedBy = userId;
            LastUtcModified = utcNow;
        }
        public void RemoveSection(Guid sectionId, Guid modifiedBy, DateTime utcNow)
        {
            var section = _sections
                .FirstOrDefault(s => s.Id == sectionId);

            if (section is null)
                throw new DomainException("Section not found.");

            _sections.Remove(section);

            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }

        public void UpdateSection(Guid sectionId, string title, string content, int displayOrder, Guid modifiedBy, DateTime utcNow)
        {
            var section = _sections
                .FirstOrDefault(s => s.Id == sectionId);
            if (section is null)
                throw new DomainException("Section not found.");
            section.Update(title, content, displayOrder, modifiedBy, utcNow);
            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }
    }
}

