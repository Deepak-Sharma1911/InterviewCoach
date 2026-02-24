using InterviewCoach.Domain.Exceptions;
using MediatR;

namespace InterviewCoach.Application.Feature.Page.Commands.PublishPage
{
    public sealed class PublishPageCommandHandler : ICommandHandler<PublishPageCommand, Unit>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public PublishPageCommandHandler(
            IPageRepository pageRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ISystemClock dateTime)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }
        public async Task<Unit> Handle(PublishPageCommand request, CancellationToken token)
        {
            var page = await _pageRepository.GetByIdWithSectionsAsync(request.PageId, token) ?? throw new NotFoundException(request.PageId);
            page.Publish(_currentUser.UserId, _dateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync(token);
            return Unit.Value;
        }
    }
}
