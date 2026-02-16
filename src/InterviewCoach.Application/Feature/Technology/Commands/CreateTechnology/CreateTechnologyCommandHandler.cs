using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Technology.Commands.CreateTechnology
{
    public class CreateTechnologyCommandHandler : ICommandHandler<CreateTechnologyCommand, Guid>
    {
        private readonly ILogger<CreateTechnologyCommandHandler> _logger;
        private readonly ITechnologyWriteRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public CreateTechnologyCommandHandler(ILogger<CreateTechnologyCommandHandler> logger, ITechnologyWriteRepository writeRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, ISystemClock dateTime)
        {
            _logger = logger;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }
        public async Task<Guid> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            var technology = Domain.Entities.Technology.Create(request.Title, request.Slug, request.DisplayOrder, _currentUser.UserId, _dateTime.UtcNow);
            await _writeRepository.AddAsync(technology, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return technology.Id;
        }
    }
}
