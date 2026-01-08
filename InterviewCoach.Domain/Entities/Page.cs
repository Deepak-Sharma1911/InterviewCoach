using InterviewCoach.Domain.Common;

namespace InterviewCoach.Domain.Entities
{
    public sealed class Page : Entity<Guid>
    {
        private readonly List<PageSection> _sections = new();
        private Page() { }
        public Guid TopicId { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Summary { get; private set; }
        public bool IsPublished { get; private set; }
        public IReadOnlyCollection<PageSection> Sections => _sections.AsReadOnly();
        public static Page Create(
            Guid topicId,
            string title,
            string slug,
            string summary)
        {
            return new Page
            {
                TopicId = topicId,
                Title = title,
                Slug = slug,
                Summary = summary,
                IsPublished = false
            };
        }
        public void Publish() => IsPublished = true;
        public void AddSection(PageSection section)
        {
            _sections.Add(section);
        }
    }

}
