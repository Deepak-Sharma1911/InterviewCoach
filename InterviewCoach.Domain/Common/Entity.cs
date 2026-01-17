using InterviewCoach.Domain.Contracts;

namespace InterviewCoach.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>, IAuditableEntity
    {
        public T Id { get; protected set; } = default!;
        public Guid CreatedBy { get; protected set; }
        public DateTime CreatedUtcDate { get; protected set; }
        public Guid? LastModifiedBy { get; protected set; }
        public DateTime LastUtcModified { get; protected set; }

        public void MarkCreated(Guid user, DateTime utcNow)
        {
            CreatedBy = user;
            CreatedUtcDate = utcNow;
            LastUtcModified = utcNow;
        }

        public void MarkModified(Guid user, DateTime utcNow)
        {
            LastUtcModified = utcNow;
            LastModifiedBy = user;
        }
        protected void SetCreated(Guid createdBy, DateTime utcNow) => MarkCreated(createdBy, utcNow);
        protected void SetModified(Guid modifiedBy, DateTime utcNow) => MarkModified(modifiedBy, utcNow);

    }

}
