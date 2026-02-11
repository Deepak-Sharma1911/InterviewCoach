using InterviewCoach.Domain.Exceptions;
using MediatR;

namespace InterviewCoach.Application.Feature.Topic.Commands.DeactivateTopic
{
    public sealed class DeactivateTopicCommandHandler : ICommandHandler<DeactivateTopicCommand>
    {
        private readonly ITopicRepository _writeRepository;
        private readonly ITopicReadRepository _readRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public DeactivateTopicCommandHandler(ITopicRepository writeRepository, ITopicReadRepository readRepository,
                                           IUnitOfWork unitOfWork,
                                           ICurrentUser currentUser,
                                           ISystemClock dateTime)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(DeactivateTopicCommand request, CancellationToken cancellationToken)
        {
            TopicDomain.Topic topic = await _readRepository.GetTopicByIdAsync(request.TopicId, cancellationToken);
            topic.Deactivate(_currentUser.UserId, _dateTime.UtcNow);
            await _writeRepository.UpdateAsync(topic, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
