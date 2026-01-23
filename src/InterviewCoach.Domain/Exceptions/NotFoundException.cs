namespace InterviewCoach.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(Guid ErrorId) : base($"Record with {ErrorId} not found ", System.Net.HttpStatusCode.NotFound)
        {

        }
        public Guid ErrorId { get; }

    }
}
