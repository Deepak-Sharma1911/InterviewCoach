using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class PageSection : Entity<Guid>
    {
        public Guid PageId { get; private set; }
        public PageSectionType SectionType { get; private set; }
        public string Title { get; private set; } = null!;
        public string Content { get; private set; } = null!;
        public int DisplayOrder { get; private set; }
        private PageSection() { } // Domain-only rehydration

        // CREATE (new)
        internal static PageSection Create(
            Guid pageId,
            PageSectionType sectionType,
            string title,
            string content,
            int displayOrder,
            Guid createdBy,
            DateTime utcNow)
        {
            Validate(title, content);

            var section = new PageSection
            {
                Id = Guid.NewGuid(),
                PageId = pageId,
                SectionType = sectionType,
                Title = title,
                Content = content,
                DisplayOrder = displayOrder
            };

            section.SetCreated(createdBy, utcNow);
            return section;
        }

        // REHYDRATE (from DB)
        public static PageSection Rehydrate(
            Guid id,
            Guid pageId,
            PageSectionType sectionType,
            string title,
            string content,
            int displayOrder,
            Guid createdBy,
            DateTime createdUtc,
            Guid? lastModifiedBy,
            DateTime? lastModifiedUtc)
        {
            var section = new PageSection
            {
                Id = id,
                PageId = pageId,
                SectionType = sectionType,
                Title = title,
                Content = content,
                DisplayOrder = displayOrder
            };

            section.SetCreated(createdBy, createdUtc);

            if (lastModifiedBy.HasValue && lastModifiedUtc.HasValue)
                section.SetModified(lastModifiedBy.Value, lastModifiedUtc.Value);

            return section;
        }

        private static void Validate(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Section title is required.");

            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("Section content is required.");
        }
        public void Update(string title, string content, int displayOrder, Guid modifiedBy, DateTime utcNow)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Section title is required.");

            Title = title;
            Content = content;
            DisplayOrder = displayOrder;

            SetModified(modifiedBy, utcNow);
        }

    }


}




