namespace InterviewCoach.Domain.Contracts
{
    public interface IEntity
    {
        Guid CreatedBy { get; }
        DateTime? CreatedUtcDate { get; }
        Guid? LastModifiedBy { get; }
        DateTime LastUtcModified { get; }
    }
    public interface IEntity<T> : IEntity
    {
        public T Id { get;}
    }
}
