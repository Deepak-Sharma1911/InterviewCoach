using MediatR;

namespace InterviewCoach.Application.Abstractions
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> where TResponse : notnull
    {
    }

    public interface ICommandHandler<Tcommand> : ICommandHandler<Tcommand, Unit> where Tcommand : ICommand<Unit>
    {

    }
}
