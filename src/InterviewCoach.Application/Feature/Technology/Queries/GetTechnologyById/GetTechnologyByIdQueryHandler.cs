using InterviewCoach.Application.Wrappers.ReadModels;
using InterviewCoach.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Technology.Queries.GetTechnologyById
{
    public class GetTechnologyByIdQueryHandler : IQueryHandler<GetTechnologyByIdQuery, TechnologyDto>
    {
        private readonly ILogger<GetTechnologyByIdQueryHandler> _logger;
        private readonly ITechnologyRepository _repository;

        public GetTechnologyByIdQueryHandler(ILogger<GetTechnologyByIdQueryHandler> logger, ITechnologyRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<TechnologyDto> Handle(GetTechnologyByIdQuery request, CancellationToken cancellationToken)
        {
            var technology = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return technology is null
                ? throw new NotFoundException("Technology not found.")
                : new TechnologyDto(technology.Id, technology.Title, technology.Slug, technology.DisplayOrder);
        }
    }
}
