using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Infrastructure.Persistence.Database;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {

    }
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<PageSection> PageSections => Set<PageSection>();
    public DbSet<Technology> Technology => Set<Technology>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

    }

}
