using MediatR;

namespace InterviewCoach.Application.Abstractions
{
    public interface IQuery<T> : IRequest<T> where T : notnull
    {
    }
}
