namespace InterviewCoach.Infrastructure.Persistence.Database.Entities;

public partial class PageSectionType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<PageSection> PageSections { get; set; } = new List<PageSection>();
}
