using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewCoach.Infrastructure.Persistence.Configurations
{
    internal class PageSectionTypeConfiguration : IEntityTypeConfiguration<PageSectionType>
    {
        public void Configure(EntityTypeBuilder<PageSectionType> entity)
        {
            entity.ToTable("PageSectionTypes", "ic");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
