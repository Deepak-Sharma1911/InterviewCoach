using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    internal class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> entity)
        {
            entity.ToTable("Pages", "ic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedUtcDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.LastUtcModified).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.Topic).WithMany(p => p.Pages)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_Pages_Topics");

        }
    }
}
