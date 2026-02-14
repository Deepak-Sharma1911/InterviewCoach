using InterviewCoach.Infrastructure.Persistence.Database.Entities;

namespace InterviewCoach.Infrastructure.Persistence.Mappings
{
    public static class TechnologyMapper
    {
        public static Domain.Entities.Technology ToDomainTechnology(Technology ef)
        {
            return Domain.Entities.Technology.Rehydrate(
                ef.Id,
                ef.Title,
                ef.Slug,
                ef.DisplayOrder,
                ef.IsActive,
                ef.CreatedBy,
                ef.CreatedUtcDate,
                ef.LastModifiedBy,
                ef.LastUtcModified

            );
        }

        public static Technology ToEntityTechnology(Domain.Entities.Technology domain)
        {
            return new Technology
            {
                Id = domain.Id,
                Title = domain.Title,
                Slug = domain.Slug,
                DisplayOrder = domain.DisplayOrder,
                IsActive = domain.IsActive,
                CreatedBy = domain.CreatedBy,
                CreatedUtcDate = domain.CreatedUtcDate,
                LastModifiedBy = domain.LastModifiedBy,
                LastUtcModified = domain.LastUtcModified
            };
        }
    }

}
