using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Application.Feature.Topic.Commands.CreateChildTopic
{
    public record CreateChildTopicCommand(Guid ParentTopicId, string Name, string Description) : IRequest<Guid>
    {

    }
}
