using InterviewCoach.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InterviewCoach.Application.Feature.Page.Commands.CreatePageSection
{
    public class AddPageSectionCommandHandler : ICommandHandler<AddPageSectionCommand, Unit>
    {
        private ILogger<AddPageSectionCommandHandler> logger;
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ISystemClock _dateTime;
        public AddPageSectionCommandHandler(ILogger<AddPageSectionCommandHandler> _logger, IPageRepository pageRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, ISystemClock dateTime)
        {
            _logger = logger;
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }
        public async Task<Unit> Handle(AddPageSectionCommand request, CancellationToken token)
        {
            var page = await _pageRepository.GetByIdAsync(request.PageId, token) ?? throw new NotFoundException(request.PageId);
            page.AddSection(request.Type,request.Title,request.Content,request.DisplayOrder,_currentUser.UserId,_dateTime.UtcNow);
            await _unitOfWork.SaveChangesAsync(token);  
            return Unit.Value;
        }
    }
}
