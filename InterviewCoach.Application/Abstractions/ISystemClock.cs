using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Application.Abstractions
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
