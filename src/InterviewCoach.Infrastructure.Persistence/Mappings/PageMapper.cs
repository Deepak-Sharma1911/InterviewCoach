using InterviewCoach.Infrastructure.Persistence.Database.Entities;

namespace InterviewCoach.Infrastructure.Persistence.Mappings
{
    internal static class PageMapper
    {
        // EF → Domain
        public static PageDomain.Page ToDomainPage(Page ef)
        {
            var sections = ef.PageSections
                .Select(PageSectionMapper.ToDomainPageSection)
                .ToList();

            return PageDomain.Page.Rehydrate(
                ef.Id,
                ef.TopicId,
                ef.Title,
                ef.Slug,
                ef.Summary,
                ef.IsPublished,
                ef.CreatedBy,
                ef.CreatedUtcDate,
                ef.LastModifiedBy,
                ef.LastUtcModified == default ? null : ef.LastUtcModified,
                sections
            );
        }

        // Domain → EF
        public static Page ToEntityPage(PageDomain.Page domain)
        {
            var ef = new Page
            {
                Id = domain.Id,
                TopicId = domain.TopicId,
                Title = domain.Title,
                Slug = domain.Slug,
                Summary = domain.Summary,
                IsPublished = domain.IsPublished,
                CreatedBy = domain.CreatedBy,
                CreatedUtcDate = domain.CreatedUtcDate,
                LastModifiedBy = domain.LastModifiedBy,
                LastUtcModified = domain.LastUtcModified
            };

            foreach (var section in domain.Sections)
                ef.PageSections.Add(PageSectionMapper.ToEntityPageSection(section));

            return ef;
        }
    }

}
