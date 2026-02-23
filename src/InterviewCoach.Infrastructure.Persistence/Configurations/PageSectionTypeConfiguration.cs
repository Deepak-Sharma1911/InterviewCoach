using InterviewCoach.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
   public class PageSectionTypeConfiguration : IEntityTypeConfiguration<PageSectionType>
    {
        public void Configure(EntityTypeBuilder<PageSectionType> entity)
        {
            entity.ToTable("PageSectionTypes", "ic");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x=>x.IsActive).HasDefaultValue(true);

            entity.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(100);

            entity.Property(x => x.IsActive)
                   .IsRequired();

            entity.Property(x => x.CreatedUtcDate)
                   .IsRequired();

            entity.Property(x => x.LastUtcModified)
                   .IsRequired();

            entity.Property(x => x.RowVersion)
                   .IsRowVersion();

            entity.HasIndex(x => x.Code)
                   .IsUnique();
        }
    }
}
