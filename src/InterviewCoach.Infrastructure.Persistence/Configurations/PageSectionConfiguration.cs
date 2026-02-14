using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    internal class PageSectionConfiguration : IEntityTypeConfiguration<PageSection>
    {
        public void Configure(EntityTypeBuilder<PageSection> entity)
        {
            entity.ToTable("PageSections", "ic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedUtcDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.LastUtcModified).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.Page).WithMany(p => p.PageSections)
                .HasForeignKey(d => d.PageId)
                .HasConstraintName("FK_PageSections_Pages");

            entity.HasOne(d => d.SectionTypeNavigation).WithMany(p => p.PageSections)
                .HasForeignKey(d => d.SectionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PageSections_SectionTypes");
        }
    }
}
