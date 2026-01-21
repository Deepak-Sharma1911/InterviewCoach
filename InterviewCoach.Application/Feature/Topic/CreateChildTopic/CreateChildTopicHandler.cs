using InterviewCoach.Application.Abstractions;
using InterviewCoach.Domain.Exceptions;
using MediatR;
using RootTopicDomain = InterviewCoach.Domain.Entities;

namespace InterviewCoach.Application.Feature.Topic.CreateChildTopic
{
    public sealed class CreateChildTopicHandler
     : IRequestHandler<CreateChildTopicCommand, Guid>
    {
        private readonly ITopicRepository _repository;
        private readonly ICurrentUser _currentUser;

        public CreateChildTopicHandler(
            ITopicRepository repository,
            ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(
            CreateChildTopicCommand request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}
