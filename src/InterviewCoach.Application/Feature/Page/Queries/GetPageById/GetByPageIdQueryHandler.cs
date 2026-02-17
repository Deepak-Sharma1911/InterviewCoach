using InterviewCoach.Application.Wrappers.ReadModels;
using InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Page.Queries.GetPageById
{
    public class GetByPageIdQueryHandler : IQueryHandler<GetByPageIdQuery, PageDto>
    {
        private readonly IPageReadRepository _readRepository;

        public GetByPageIdQueryHandler(IPageReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<PageDto> Handle(GetByPageIdQuery request, CancellationToken token)
        {
            var page = await _readRepository.GetByIdAsync(request.pageId, token);

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
                        Type = (PageSectionType)x.SectionType,
                        Title = x.Title,
                        Content = x.Content,
                        DisplayOrder = x.DisplayOrder
                    })
                    .ToList()
            };
        }
    } 
}
