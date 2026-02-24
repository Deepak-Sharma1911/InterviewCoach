using InterviewCoach.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Page.Commands.RemovePageSection
{
    public sealed class RemovePageSectionHandler : ICommandHandler<RemovePageSectionCommand, Unit>
    {
        private readonly ILogger<RemovePageSectionHandler> _logger;
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemClock _systemClock;
        private readonly ICurrentUser _currentUser;

        public RemovePageSectionHandler(ILogger<RemovePageSectionHandler> logger, IPageRepository pageRepository, IUnitOfWork unitOfWork, ISystemClock systemClock, ICurrentUser currentUser)
        {
            _logger = logger;
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            _systemClock = systemClock;
            _currentUser = currentUser;
        }
        public async Task<Unit> Handle(RemovePageSectionCommand request, CancellationToken token)
        {
            var page = await _pageRepository.GetByIdWithSectionsAsync(request.PageId, token) ?? throw new NotFoundException("Page not found.");
            page.RemoveSection(request.SectionId, _currentUser.UserId, _systemClock.UtcNow);
            await _unitOfWork.SaveChangesAsync(token);
            return Unit.Value;
        }
    }
}
