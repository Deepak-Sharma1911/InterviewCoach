using InterviewCoach.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Page.Commands.UpdatePageSection
{
    public sealed class UpdatePageSectionCommandHandler : ICommandHandler<UpdatePageSectionCommand, Unit>
    {
        private readonly ILogger<UpdatePageSectionCommandHandler> _logger;
        private readonly IPageReadRepository _readRepository;
        private readonly IPageWriteRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;

        public UpdatePageSectionCommandHandler(ILogger<UpdatePageSectionCommandHandler> logger, IPageReadRepository readRepository, IPageWriteRepository writeRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ISystemClock dateTime)
        {
            _logger = logger;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(UpdatePageSectionCommand request, CancellationToken token)
        {
            var page = await _readRepository.GetByIdAsync(request.PageId, token);
            if (page is null)
                throw new NotFoundException("Page not found.");

            page.UpdateSection(
                request.SectionId,
                request.Title,
                request.Content,
                request.DisplayOrder,
                _currentUser.UserId,
                _dateTime.UtcNow);

            await _writeRepository.UpdateAsync(page, token);
            await _unitOfWork.SaveChangesAsync(token);
            return Unit.Value;
        }
    }
}
