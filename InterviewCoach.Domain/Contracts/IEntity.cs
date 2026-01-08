namespace InterviewCoach.Domain.Contracts
{
    public interface IEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime? CreatedUtcDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime LastUtcModified { get; set; }
    }
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
