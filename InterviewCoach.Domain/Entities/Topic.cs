using InterviewCoach.Domain.Common;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Topic : Entity<Guid>
    {
        private Topic() { }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public Guid? ParentTopicId { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }
        public Page Page { get; private set; }
        public static Topic CreateRoot(string title, string slug, int displayOrder)
        {
            return new Topic
            {
                Title = title,
                Slug = slug,
                DisplayOrder = displayOrder,
                IsActive = true
            };
        }
        public static Topic CreateChild(string title, string slug, Guid parentId, int displayOrder)
        {
            return new Topic
            {
                Title = title,
                Slug = slug,
                ParentTopicId = parentId,
                DisplayOrder = displayOrder,
                IsActive = true
            };
        }
        public void Deactivate() => IsActive = false;
    }

}
