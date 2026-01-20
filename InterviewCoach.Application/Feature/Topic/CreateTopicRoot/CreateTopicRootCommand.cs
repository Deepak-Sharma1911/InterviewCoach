using MediatR;

namespace InterviewCoach.Application.Feature.Topic.CreateTopicRoot
{
    public sealed record CreateTopicCommand(string Title, string Slug, int DisplayOrder, Guid? ParentTopicId) : IRequest<CreateTopicResult>;

    public sealed record CreateTopicResult(Guid Id, string Title, string Slug);

}
