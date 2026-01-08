using InterviewCoach.Domain.Contracts;

namespace InterviewCoach.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T? Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? CreatedUtcDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime LastUtcModified { get; set; }
    }
}
