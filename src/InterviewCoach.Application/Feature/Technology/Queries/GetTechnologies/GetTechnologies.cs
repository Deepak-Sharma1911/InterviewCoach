using InterviewCoach.Application.Wrappers.ReadModels;

namespace InterviewCoach.Application.Feature.Technology.Queries.GetTechnologies
{
    public sealed record GetTechnologiesQuery(): IQuery<IReadOnlyList<TechnologyDto>>;

}
