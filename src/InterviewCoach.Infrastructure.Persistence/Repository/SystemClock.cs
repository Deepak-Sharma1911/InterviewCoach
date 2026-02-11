namespace InterviewCoach.Infrastructure.Persistence.Repository
{
    public sealed class SystemClock : ISystemClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
