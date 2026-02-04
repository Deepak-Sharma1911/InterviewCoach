namespace InterviewCoach.Application.Feature.Topic.Commands.CreateRootTopic
{
    public class CreateRootTopicCommandHandler : ICommandHandler<CreateRootTopicCommand, Guid>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemClock _dateTimeProvider;
        private readonly ICurrentUser _userContextService;

        public CreateRootTopicCommandHandler(ITopicRepository repo, IUnitOfWork uow, ISystemClock clock, ICurrentUser user)
        {
            _topicRepository = repo;
            _unitOfWork = uow;
            _dateTimeProvider = clock;
            _userContextService = user;
        }

        public async Task<Guid> Handle(CreateRootTopicCommand request, CancellationToken cancellationToken)
        {

            var topic = TopicDomain.Topic.Create(request.Title, request.Slug, request.DisplayOrder, request.ParentTopicId, _userContextService.UserId, _dateTimeProvider.UtcNow);
            await _topicRepository.AddAsync(topic, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return topic.Id;
        }
    }
}
