using InterviewCoach.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Technology.Commands.DeleteTechnology
{
    public sealed class DeleteTechnologyCommandHandler : ICommandHandler<DeleteTechnologyCommand>
    {
        private readonly ILogger<DeleteTechnologyCommandHandler> _logger;
        private readonly ITechnologyRepository _techRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public DeleteTechnologyCommandHandler(
            ILogger<DeleteTechnologyCommandHandler> logger,
            ITechnologyRepository techRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ISystemClock dateTime)
        {
            _logger = logger;
            _techRepository = techRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            var technology = await _techRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Technology not found.");
            technology.Deactivate(_currentUser.UserId, _dateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
