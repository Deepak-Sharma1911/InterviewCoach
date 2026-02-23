using InterviewCoach.Application.Wrappers.ReadModels;
using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Page.Queries.GetPageById
{
    public class GetByPageIdQueryHandler : IQueryHandler<GetByPageIdQuery, PageDto>
    {
        private readonly IPageRepository _pageRepository;

        public GetByPageIdQueryHandler(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<PageDto> Handle(GetByPageIdQuery request, CancellationToken token)
        {
            var page = await _pageRepository.GetByIdWithSectionsAsync(request.pageId, token);
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
