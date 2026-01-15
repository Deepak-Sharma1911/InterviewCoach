using InterviewCoach.Domain.Contracts;

namespace InterviewCoach.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>, IAuditableEntity
    {
        public T Id { get; protected set; } = default!;
        public Guid CreatedBy { get; protected set; }
        public DateTime? CreatedUtcDate { get; protected set; }
        public Guid? LastModifiedBy { get; protected set; }
        public DateTime LastUtcModified { get; protected set; }

        public void MarkCreated(DateTime utcNow)
        {
            CreatedUtcDate = utcNow;
            LastUtcModified = utcNow;
        }

        public void MarkModified(DateTime utcNow)
        {
            LastUtcModified = utcNow;
        }

        protected void SetCreated(Guid createdBy, DateTime utcNow)
        {
            CreatedBy = createdBy;
            MarkCreated(utcNow);
        }

        protected void SetModified(Guid modifiedBy, DateTime utcNow)
        {
            LastModifiedBy = modifiedBy;
            MarkModified(utcNow);
        }
    }

}
