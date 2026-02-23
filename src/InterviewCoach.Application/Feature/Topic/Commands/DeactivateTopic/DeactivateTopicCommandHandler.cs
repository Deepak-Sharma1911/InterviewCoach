using MediatR;

namespace InterviewCoach.Application.Feature.Topic.Commands.DeactivateTopic
{
    public sealed class DeactivateTopicCommandHandler : ICommandHandler<DeactivateTopicCommand>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public DeactivateTopicCommandHandler(ITopicRepository topicRepository,
                                           IUnitOfWork unitOfWork,
                                           ICurrentUser currentUser,
                                           ISystemClock dateTime)
        {
            _topicRepository = topicRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(DeactivateTopicCommand request, CancellationToken cancellationToken)
        {
            TopicDomain.Topic topic = await _topicRepository.GetByIdAsync(request.TopicId, cancellationToken);
            topic.Deactivate(_currentUser.UserId, _dateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
