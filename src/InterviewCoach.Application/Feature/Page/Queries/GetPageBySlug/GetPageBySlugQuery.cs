using InterviewCoach.Application.Feature.Topic.ReadModels;
using MediatR;

namespace InterviewCoach.Application.Feature.Page.Queries.GetPageBySlug
{
    public sealed record GetPageBySlugQuery(string Slug): IQuery<PageDto>;

}
