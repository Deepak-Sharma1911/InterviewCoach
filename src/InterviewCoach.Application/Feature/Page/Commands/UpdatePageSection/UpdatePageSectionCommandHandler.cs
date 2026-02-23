using InterviewCoach.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Page.Commands.UpdatePageSection
{
    public sealed class UpdatePageSectionCommandHandler : ICommandHandler<UpdatePageSectionCommand, Unit>
    {
        private readonly ILogger<UpdatePageSectionCommandHandler> _logger;
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public UpdatePageSectionCommandHandler(ILogger<UpdatePageSectionCommandHandler> logger, IPageRepository pageRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ISystemClock dateTime)
        {
            _logger = logger;
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(UpdatePageSectionCommand request, CancellationToken token)
        {
            var page = await _pageRepository.GetByIdWithSectionsAsync(request.PageId, token) ?? throw new NotFoundException("Page not found.");
            page.UpdateSection(
                request.SectionId,
                request.Title,
                request.Content,
                request.DisplayOrder,
                _currentUser.UserId,
                _dateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync(token);
            return Unit.Value;
        }
    }
}
