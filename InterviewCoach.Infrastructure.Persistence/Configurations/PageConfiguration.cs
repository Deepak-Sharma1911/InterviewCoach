using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    internal class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> entity)
        {
            entity.HasIndex(e => e.Slug, "UX_Pages_Slug").IsUnique();
            entity.HasIndex(e => e.TopicId, "UX_Pages_TopicId").IsUnique();
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedUtcDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.LastUtcModified).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.HasOne(d => d.Topic).WithOne(p => p.Page)
                .HasForeignKey<Page>(d => d.TopicId)
                .HasConstraintName("FK_Pages_Topics");

        }
    }
}
