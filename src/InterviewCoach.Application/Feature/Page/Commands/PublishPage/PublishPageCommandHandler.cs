using InterviewCoach.Domain.Exceptions;
using MediatR;

namespace InterviewCoach.Application.Feature.Page.Commands.PublishPage
{
    public sealed class PublishPageCommandHandler : ICommandHandler<PublishPageCommand, Unit>
    {
        private readonly IPageReadRepository _readRepository;
        private readonly IPageWriteRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public PublishPageCommandHandler(
            IPageReadRepository readRepository,
            IPageWriteRepository writeRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ISystemClock dateTime)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }
        public async Task<Unit> Handle(PublishPageCommand request, CancellationToken token)
        {
            var page = await _readRepository.GetByIdAsync(request.PageId, token);

            if (page is null)
                throw new NotFoundException(request.PageId);

            page.Publish(_currentUser.UserId, _dateTime.UtcNow);

            await _writeRepository.UpdateAsync(page, token);
            await _unitOfWork.SaveChangesAsync(token);
            return Unit.Value;
        }
    }
}
