namespace InterviewCoach.Infrastructure.Persistence.Database.Entities;

public partial class Topic
{
    public Guid Id { get; set; }

    public Guid TechId { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public Guid? ParentTopicId { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedUtcDate { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime LastUtcModified { get; set; }

    public virtual ICollection<Topic> InverseParentTopic { get; set; } = new List<Topic>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual Topic ParentTopic { get; set; }

    public virtual Technology Tech { get; set; }
}

