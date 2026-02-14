using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Technology : Entity<Guid>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        private readonly List<Topic> _topics = new();
        public IReadOnlyCollection<Topic> Topics => _topics.AsReadOnly();
        //For Rehydration
        private Technology() { }

        private Technology(Guid id, string title, string slug, int displayOrder, Guid createdBy, DateTime createdUtc)
        {
            Id = id;
            SetTitle(title);
            SetSlug(slug);
            DisplayOrder = displayOrder;
            IsActive = true;
            CreatedBy = createdBy;
            CreatedUtcDate = createdUtc;
        }

        public static Technology Create(string title, string slug, int displayOrder, Guid createdBy, DateTime utcNow)
        {
            return new Technology(
                Guid.NewGuid(),
                title,
                slug,
                displayOrder,
                createdBy,
                utcNow);
        }

        public static Technology Rehydrate(Guid id, string title, string slug, int displayOrder,
                                              bool isActive,
                                              Guid createdBy,
                                              DateTime createdUtc,
                                              Guid? lastModifiedBy,
                                              DateTime lastModifiedUtc)
        {
            var tech = new Technology
            {
                Id = id,
                Title = title,
                Slug = slug,
                DisplayOrder = displayOrder,
                IsActive = isActive,
                CreatedBy = createdBy,
                CreatedUtcDate = createdUtc,
                LastModifiedBy = lastModifiedBy,
                LastUtcModified = lastModifiedUtc
            };

            return tech;
        }

        public void Update(string title, string slug, int displayOrder, Guid modifiedBy, DateTime utcNow)
        {
            SetTitle(title);
            SetSlug(slug);
            DisplayOrder = displayOrder;
            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }

        public void Activate(Guid modifiedBy, DateTime utcNow)
        {
            IsActive = true;
            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }

        public void Deactivate(Guid modifiedBy, DateTime utcNow)
        {
            IsActive = false;
            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Technology title is required.");

            Title = title.Trim();
        }

        private void SetSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Technology slug is required.");

            Slug = slug.Trim().ToLowerInvariant();
        }
    }
}
