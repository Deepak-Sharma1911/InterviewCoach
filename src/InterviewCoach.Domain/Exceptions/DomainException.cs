using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Domain.Exceptions
{
    public class DomainException : BaseException
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
