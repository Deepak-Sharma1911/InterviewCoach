using InterviewCoach.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    internal class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> entity)
        {
            entity.ToTable("Pages", "ic");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.CreatedUtcDate).IsRequired();

            entity.Property(e => e.LastUtcModified).IsRequired();

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.Property(e => e.RowVersion)
                  .IsRequired()
                  .IsRowVersion()
                  .IsConcurrencyToken();

            entity.Property(e => e.Slug)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Summary).HasMaxLength(500);

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasIndex(x => new { x.TopicId, x.Slug })
                  .IsUnique();

            entity.HasOne<Topic>()
              .WithMany(t => t.Pages)
              .HasForeignKey(x => x.TopicId)
              .OnDelete(DeleteBehavior.Cascade)
              .HasConstraintName("FK_Pages_Topics");

        }
    }
}
