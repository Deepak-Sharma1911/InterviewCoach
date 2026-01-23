using System;
using System.Collections.Generic;

namespace InterviewCoach.Infrastructure.Persistence.Database.Entities;

public partial class Page
{
    public Guid Id { get; set; }

    public Guid TopicId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Summary { get; set; }

    public bool IsPublished { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedUtcDate { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime LastUtcModified { get; set; }

    public virtual ICollection<PageSection> PageSections { get; } = new List<PageSection>();

    public virtual Topic Topic { get; set; } = null!;
}
