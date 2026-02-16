using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> entity)
        {
            entity.ToTable("Topics", "ic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedUtcDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUtcModified).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.ParentTopic).WithMany(p => p.InverseParentTopic)
                .HasForeignKey(d => d.ParentTopicId)
                .HasConstraintName("FK_Topics_Parent");

            entity.HasOne(d => d.Tech).WithMany(p => p.Topics)
                .HasForeignKey(d => d.TechId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Topics_Technology");
        }
    }
}
