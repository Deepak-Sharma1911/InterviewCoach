using InterviewCoach.Infrastructure.Persistence.Database.Entities;

namespace InterviewCoach.Infrastructure.Persistence.Mappings
{
    internal static class PageSectionMapper
    {
        // EF → Domain
        public static PageSectionDomain.PageSection ToDomainPageSection(PageSection ef)
        {
            return PageSectionDomain.PageSection.Rehydrate(
                ef.Id,
                ef.PageId,
                (PageSectionDomain.PageSectionType)ef.SectionType,
                ef.Title,
                ef.Content,
                ef.DisplayOrder,
                ef.CreatedBy,
                ef.CreatedUtcDate,
                ef.LastModifiedBy,
                ef.LastUtcModified == default ? null : ef.LastUtcModified
            );
        }

        // Domain → EF
        public static PageSection ToEntityPageSection(PageSectionDomain.PageSection domain)
        {
            return new PageSection
            {
                Id = domain.Id,
                PageId = domain.PageId,
                SectionType = (int)domain.SectionType,
                Title = domain.Title,
                Content = domain.Content,
                DisplayOrder = domain.DisplayOrder,
                CreatedBy = domain.CreatedBy,
                CreatedUtcDate = domain.CreatedUtcDate,
                LastModifiedBy = domain.LastModifiedBy,
                LastUtcModified = domain.LastUtcModified
            };
        }
    }

}
