using InterviewCoach.Application.Wrappers.ReadModels;

namespace InterviewCoach.Application.Feature.Technology.Queries.GetTechnologyById
{
    public sealed record GetTechnologyByIdQuery(Guid Id): IQuery<TechnologyDto>;

}
