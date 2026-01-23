using MediatR;

namespace InterviewCoach.Application.Abstractions
{
    public interface ICommand : ICommand<Unit>
    {

    }
    public interface ICommand<TResponse> : IRequest<TResponse> where TResponse : notnull
    {

    }
}
