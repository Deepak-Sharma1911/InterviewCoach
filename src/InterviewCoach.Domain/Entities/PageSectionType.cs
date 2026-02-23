using InterviewCoach.Domain.Common;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Entities
{
    public sealed class PageSectionType : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool IsActive { get; private set; }
        public byte[] RowVersion { get; private set; }
        private PageSectionType() { } // EF Core

        private PageSectionType(
            string name,
            string code,
            Guid createdBy,
            DateTime utcNow)
        {
            Id = Guid.NewGuid();
            Name = name;
            Code = code;
            IsActive = true;
            CreatedBy = createdBy;
            CreatedUtcDate = utcNow;
            LastUtcModified = utcNow;
        }

        public static PageSectionType Create(
            string name,
            string code,
            Guid createdBy,
            DateTime utcNow)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Section type name is required.");

            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Section type code is required.");

            return new PageSectionType(name, code, createdBy, utcNow);
        }

        public void Rename(string name, Guid modifiedBy, DateTime utcNow)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Section type name cannot be empty.");

            Name = name;
            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }

        public void Deactivate(Guid modifiedBy, DateTime utcNow)
        {
            IsActive = false;
            LastModifiedBy = modifiedBy;
            LastUtcModified = utcNow;
        }
    }
    public enum PageSectionTypeEnum
    {
        Text = 1,
        Code = 2,
        InfoBox = 3,
        Comparison = 4,
        InterviewQuestions = 5
    }

}
