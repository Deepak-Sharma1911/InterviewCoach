using InterviewCoach.Application.Wrappers.ReadModels;

namespace InterviewCoach.Application.Feature.Page.Queries.GetPageById
{
    public sealed record GetByPageIdQuery(Guid pageId) : IQuery<PageDto>;
}
