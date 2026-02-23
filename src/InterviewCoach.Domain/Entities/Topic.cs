using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Topic : Entity<Guid>
    {
        public Guid TechnologyId { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public Guid? ParentTopicId { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }
        public byte[] RowVersion { get; private set; }

        private readonly List<Page> _pages = new();
        public IReadOnlyCollection<Page> Pages => _pages;
        private Topic() { }
        internal Topic(Guid technologyId, string title, string slug, Guid? parentTopicId, int displayOrder, Guid userId, DateTime utcNow)
        {
            Id = Guid.NewGuid();
            TechnologyId = technologyId;
            Title = title;
            Slug = slug;
            ParentTopicId = parentTopicId;
            DisplayOrder = displayOrder;
            IsActive = true;
            CreatedBy = userId;
            CreatedUtcDate = utcNow;
            LastUtcModified = utcNow;
        }
        public static Topic Create(Guid technologyId, string title, string slug, Guid? parentTopicId, int displayOrder, Guid userId, DateTime utcNow)
        {
            return new Topic
            {
                Id = Guid.NewGuid(),
                TechnologyId = technologyId,
                Title = title,
                Slug = slug,
                ParentTopicId = parentTopicId,
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
        public Page AddPage(string title, string slug, string summary, Guid createdBy, DateTime utcNow)
        {
            if (_pages.Any(p => p.Slug == slug))
                throw new DomainException("Duplicate slug in topic.");

            var page = Page.Create(
                Id,
                title,
                slug,
                summary,
                createdBy,
                utcNow);

            _pages.Add(page);

            return page;
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





