using InterviewCoach.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> entity)
        {
            entity.ToTable("Topics", "ic");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.CreatedUtcDate).IsRequired();

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.Property(e => e.LastUtcModified).IsRequired();

            entity.Property(e => e.RowVersion)
                  .IsRequired()
                  .IsRowVersion()
                  .IsConcurrencyToken();

            entity.Property(e => e.Slug)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasIndex(x => x.Slug)
                  .IsUnique();

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasOne<Topic>()
                  .WithMany()
                  .HasForeignKey(d => d.ParentTopicId)
                  .HasConstraintName("FK_Topics_Parent")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(x => x.Pages)
                  .WithOne()
                  .HasForeignKey(y => y.TopicId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Technology>()
                  .WithMany(p => p.Topics)
                  .HasForeignKey(d => d.TechnologyId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Topics_Technology");

            entity.Navigation(x => x.Pages)
                  .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
