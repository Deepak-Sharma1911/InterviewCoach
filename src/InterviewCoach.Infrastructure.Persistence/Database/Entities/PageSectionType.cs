using System;
using System.Collections.Generic;

namespace InterviewCoach.Infrastructure.Persistence.Database.Entities;

public partial class PageSectionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PageSection> PageSections { get; } = new List<PageSection>();
}
