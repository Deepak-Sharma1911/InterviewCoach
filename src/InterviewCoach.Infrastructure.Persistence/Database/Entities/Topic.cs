using System;
using System.Collections.Generic;

namespace InterviewCoach.Infrastructure.Persistence.Database.Entities;

public partial class Topic
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public Guid? ParentTopicId { get; set; }

    public int DisplayOrder { get; set; }

    public bool? IsActive { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedUtcDate { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime LastUtcModified { get; set; }

    public virtual ICollection<Topic> InverseParentTopic { get; } = new List<Topic>();

    public virtual Page? Page { get; set; }

    public virtual Topic? ParentTopic { get; set; }
}
