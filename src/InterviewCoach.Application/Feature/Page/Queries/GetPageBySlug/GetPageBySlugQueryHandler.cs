using InterviewCoach.Application.Wrappers.ReadModels;
using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Page.Queries.GetPageBySlug
{
    public sealed class GetPageBySlugQueryHandler : IQueryHandler<GetPageBySlugQuery, PageDto>
    {
        private readonly IPageRepository _pageRepository;

        public GetPageBySlugQueryHandler(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<PageDto> Handle(GetPageBySlugQuery request, CancellationToken token)
        {
            var page = await _pageRepository.GetBySlugAsync(request.Slug, token);

            if (page is null)
                return null;

            return new PageDto
            {
                Id = page.Id,
                Title = page.Title,
                Slug = page.Slug,
                Summary = page.Summary,
                IsPublished = page.IsPublished,
                Sections = page.Sections
                    .OrderBy(x => x.DisplayOrder)
                    .Select(x => new PageSectionDto
                    {
                        Id = x.Id,
                        Type = (PageSectionTypeEnum)x.SectionType,
                        Title = x.Title,
                        Content = x.Content,
                        DisplayOrder = x.DisplayOrder
                    })
                    .ToList()
            };
        }
    }
}
