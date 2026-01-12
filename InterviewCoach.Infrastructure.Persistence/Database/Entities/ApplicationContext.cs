using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InterviewCoach.Infrastructure.Persistence.Database.Entities;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Page> Pages { get; set; }
    public virtual DbSet<PageSection> PageSections { get; set; }
    public virtual DbSet<PageSectionType> PageSectionTypes { get; set; }
    public virtual DbSet<Topic> Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DEL1-LHP-N77307;Database=InterviewCoachDB;Trusted_Connection=True;TrustServerCertificate=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
