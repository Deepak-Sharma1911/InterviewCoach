using InterviewCoach.Application.Wrappers.ReadModels;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Technology.Queries.GetTechnologies
{
    public class GetTechnologiesQueryHandler : IQueryHandler<GetTechnologiesQuery, IReadOnlyList<TechnologyDto>>
    {
        private readonly ILogger<GetTechnologiesQueryHandler> _logger;
        private readonly ITechnologyRepository _repository;
        public GetTechnologiesQueryHandler(ILogger<GetTechnologiesQueryHandler> logger, ITechnologyRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<IReadOnlyList<TechnologyDto>> Handle(GetTechnologiesQuery request, CancellationToken cancellationToken)
        {
            var technologies = await _repository.GetAllAsync(cancellationToken);
            return technologies.Select(t => new TechnologyDto(t.Id, t.Title, t.Slug, t.DisplayOrder)).ToList();
        }
    }
}
