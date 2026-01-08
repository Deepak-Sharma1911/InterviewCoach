using InterviewCoach.Domain.Common;

namespace InterviewCoach.Domain.Entities
{
    public sealed class PageSection : Entity<Guid>
    {
        private PageSection() { }
        public Guid PageId { get; private set; }
        public PageSectionType SectionType { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int DisplayOrder { get; private set; }
        public static PageSection Create(
            Guid pageId,
            PageSectionType type,
            string title,
            string content,
            int displayOrder)
        {
            return new PageSection
            {
                PageId = pageId,
                SectionType = type,
                Title = title,
                Content = content,
                DisplayOrder = displayOrder
            };
        }
    }

}
