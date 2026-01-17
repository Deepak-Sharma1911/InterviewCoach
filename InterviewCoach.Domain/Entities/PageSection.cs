using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Entities;
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

        private PageSection() { } // EF rehydration

        internal static PageSection Create(
            Guid pageId,
            PageSectionType sectionType,
            string title,
            string content,
            int displayOrder,
            Guid createdBy,
            DateTime utcNow)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Section title is required.");

            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("Section content is required.");

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
    }

}




