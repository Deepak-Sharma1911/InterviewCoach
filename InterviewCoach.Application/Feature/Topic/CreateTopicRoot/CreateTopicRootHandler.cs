using InterviewCoach.Application.Abstractions;
using InterviewCoach.Domain.Exceptions;
using RootTopicDomain = InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Topic.CreateTopicRoot
{
    public class CreateTopicRootHandler : ICommandHandler<CreateRootTopicCommand, Guid>
    {
        private readonly ITopicRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUser _currentUser;

        public CreateTopicRootHandler(ITopicRepository repository, IUnitOfWork uow, ICurrentUser currentUser)
        {
            _repository = repository;
            _uow = uow;
            _currentUser = currentUser;
        }
        public async Task<Guid> Handle(CreateRootTopicCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.SlugExistsAsync(request.Slug, cancellationToken))
                throw new DomainException("Topic slug already exists.");
            var topic = RootTopicDomain.Topic.CreateRoot(
                title: request.Title,
                slug: request.Slug,
                displayOrder: request.DisplayOrder,
                createdBy: _currentUser.UserId,
                utcNow: DateTime.UtcNow);
            await _repository.AddAsync(topic, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
            return topic.Id!;
        }
    }
}
