namespace InterviewCoach.Application.Abstractions
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
