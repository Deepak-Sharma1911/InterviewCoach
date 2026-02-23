using InterviewCoach.Application.Abstractions;
using InterviewCoach.Application.Feature.Page.Commands.RemovePageSection;
using InterviewCoach.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Page.Commands.RemovePage
{
    public class RemovePageHandler : ICommandHandler<RemovePageCommand, Unit>
    {
        private readonly ILogger<RemovePageSectionHandler> _logger;
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemClock _systemClock;
        private readonly ICurrentUser _currentUser;
        public RemovePageHandler(ILogger<RemovePageSectionHandler> logger, IPageRepository pageRepository, IUnitOfWork unitOfWork, ISystemClock systemClock, ICurrentUser currentUser)
        {
            _logger = logger;
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            _systemClock = systemClock;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(RemovePageCommand request, CancellationToken token)
        {
            _logger.LogInformation("Remove the Page");
            await _pageRepository.DeleteAsync(request.PageId, token);
            await _unitOfWork.SaveChangesAsync(token);
            return Unit.Value;
        }
    }
}
