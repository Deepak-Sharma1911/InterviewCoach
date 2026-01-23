using InterviewCoach.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Application.Feature.Topic.CreateChildTopic
{
    public sealed record CreateChildTopicCommand(
    Guid ParentTopicId,
    string Title,
    string Slug,
    int DisplayOrder
) : ICommand<Guid>;
}
