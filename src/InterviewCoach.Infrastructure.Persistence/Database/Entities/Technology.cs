namespace InterviewCoach.Infrastructure.Persistence.Database.Entities
{
    public partial class Technology
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedUtcDate { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public DateTime LastUtcModified { get; set; }

        public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}
