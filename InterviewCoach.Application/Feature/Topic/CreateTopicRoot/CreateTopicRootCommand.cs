using InterviewCoach.Application.Abstractions;
using MediatR;

namespace InterviewCoach.Application.Feature.Topic.CreateTopicRoot
{
    public sealed record CreateTopicCommand(string Title, string Slug, int DisplayOrder, Guid? ParentTopicId) : ICommand<CreateTopicResult>;

    public sealed record CreateTopicResult(Guid Id, string Title, string Slug);

}
