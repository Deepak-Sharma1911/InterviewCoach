using InterviewCoach.Application.Abstractions;
using MediatR;

namespace InterviewCoach.Application.Behavious
{
    internal class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {

        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!typeof(TRequest).Name.EndsWith("Command"))
            {
                await Console.Out.WriteLineAsync($"Starting transaction for {typeof(TRequest).Name}");
                return await next(cancellationToken);
            }
            var response = await next(cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}
