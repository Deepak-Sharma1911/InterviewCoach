using InterviewCoach.Application.Feature.Page.Commands.RemovePageSection;
using InterviewCoach.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Page.Commands.RemovePage
{
    public  class RemovePageHandler:ICommandHandler<RemovePageCommand,Unit>
    {
        private readonly ILogger<RemovePageSectionHandler> _logger;
        private readonly IPageReadRepository _readRepository;
        private readonly IPageWriteRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemClock _systemClock;
        private readonly ICurrentUser _currentUser;
        public RemovePageHandler(ILogger<RemovePageSectionHandler> logger, IPageReadRepository readRepository, IPageWriteRepository writeRepository, IUnitOfWork unitOfWork, ISystemClock systemClock, ICurrentUser currentUser)
        {
            _logger = logger;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _systemClock = systemClock;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(RemovePageCommand request, CancellationToken token)
        {
            var page = await _readRepository.GetByIdAsync(request.PageId, token);
            if (page is null)
                throw new NotFoundException("Page not found.");
            await _writeRepository.RemoveAsync(page, token);
            await _unitOfWork.SaveChangesAsync(token);
            return Unit.Value;
        }
    }
}
