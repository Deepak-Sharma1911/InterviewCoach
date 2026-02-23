using InterviewCoach.Domain.Common;

namespace InterviewCoach.Domain.Entities
{
    public sealed class PageSection : Entity<Guid>
    {
        public Guid PageId { get; private set; }
        public int SectionType { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }
        public byte[] RowVersion { get; private set; }
        private PageSection() { }
        internal PageSection(Guid pageId, int sectionType, string title, string content, int displayOrder, Guid userId, DateTime utcNow)
        {
            Id = Guid.NewGuid();
            PageId = pageId;
            SectionType = sectionType;
            Title = title;
            Content = content;
            DisplayOrder = displayOrder;
            CreatedBy = userId;
            CreatedUtcDate = utcNow;
            LastUtcModified = utcNow;
        }
        internal static PageSection Create(Guid pageId, int sectionType, string title, string content, int displayOrder, Guid userId, DateTime utcNow)
        {
            return new PageSection
            {
                Id = Guid.NewGuid(),
                PageId = pageId,
                SectionType = sectionType,
                Title = title,
                Content = content,
                DisplayOrder = displayOrder,
                CreatedBy = userId,
                CreatedUtcDate = utcNow,
                LastUtcModified = utcNow
            };
        }
        public void Update(string title, string content, int displayOrder, Guid modifiedBy, DateTime utcNow)
        {
            Title = title;
            Content = content;
            DisplayOrder = displayOrder;
            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }

    }
}




