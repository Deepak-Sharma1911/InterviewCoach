using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Domain.Contracts
{
    public interface IAuditableEntity
    {
        void MarkCreated(Guid userId, DateTime utcNow);
        void MarkModified(Guid userId, DateTime utcNow);
    }
}
