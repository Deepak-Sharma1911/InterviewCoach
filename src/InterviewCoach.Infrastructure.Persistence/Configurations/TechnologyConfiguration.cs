using InterviewCoach.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> entity)
        {
            entity.HasKey(e => e.Id)
                  .HasName("PK_Technologies");

            entity.ToTable("Technology", "ic");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.CreatedUtcDate).IsRequired();

            entity.Property(e => e.IsActive)
                  .HasDefaultValue(true);

            entity.Property(e => e.LastUtcModified)
                  .IsRequired();

            entity.Property(e => e.RowVersion)
                  .IsRequired()
                  .IsRowVersion()
                  .IsConcurrencyToken();

            entity.Property(e => e.Slug)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasIndex(x => x.Slug).IsUnique();
        }
    }
}
