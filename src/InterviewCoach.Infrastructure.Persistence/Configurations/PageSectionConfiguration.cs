using InterviewCoach.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    internal class PageSectionConfiguration : IEntityTypeConfiguration<PageSection>
    {
        public void Configure(EntityTypeBuilder<PageSection> entity)
        {
            entity.ToTable("PageSections", "ic");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Content).IsRequired();

            entity.Property(e => e.CreatedUtcDate).IsRequired();

            entity.Property(e => e.LastUtcModified).IsRequired();

            entity.Property(e => e.RowVersion)
                  .IsRequired()
                  .IsRowVersion()
                  .IsConcurrencyToken();

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasIndex(x => new { x.PageId, x.DisplayOrder})
                  .IsUnique();

            entity.HasOne<Page>()
                  .WithMany(p => p.Sections)
                  .HasForeignKey(x => x.PageId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_PageSections_Pages");

            entity.HasOne<PageSectionType>().WithMany()
                  .HasForeignKey(d => d.SectionType)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_PageSections_SectionTypes");
        }
    }
}
