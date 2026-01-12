using System;
using System.Collections.Generic;

namespace InterviewCoach.Infrastructure.Persistence.Database.Entities;

public partial class PageSection
{
    public Guid Id { get; set; }

    public Guid PageId { get; set; }

    public int SectionType { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedUtcDate { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime LastUtcModified { get; set; }

    public virtual Page Page { get; set; } = null!;

    public virtual PageSectionType SectionTypeNavigation { get; set; } = null!;
}
