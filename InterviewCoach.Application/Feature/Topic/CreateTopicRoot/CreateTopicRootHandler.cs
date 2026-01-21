using InterviewCoach.Application.Abstractions;
using InterviewCoach.Domain.Exceptions;
using TopicRootEntity = InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Topic.CreateTopicRoot
{
    public class CreateTopicRootHandler : ICommandHandler<CreateTopicCommand, CreateTopicResult>
    {
        private readonly ITopicRepository _repository;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _clock;

        public CreateTopicRootHandler(ITopicRepository repository, ICurrentUser currentUser, ISystemClock clock)
        {
            _repository = repository;
            _currentUser = currentUser;
            _clock = clock;
        }
        public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.SlugExistsAsync(request.Slug, cancellationToken))
                throw new DomainException("Topic slug already exists.");
            TopicRootEntity.Topic topic = TopicRootEntity.Topic.Create(
                                  title: request.Title,
                                  slug: request.Slug,
                                  displayOrder: request.DisplayOrder,
                                  parentTopicId: null,
                                  createdBy: _currentUser.UserId,
                                  utcNow: _clock.UtcNow
                                  );
            await _repository.AddAsync(topic, cancellationToken);
            return new CreateTopicResult(topic.Id, topic.Title, topic.Slug);
        }
    }
}
