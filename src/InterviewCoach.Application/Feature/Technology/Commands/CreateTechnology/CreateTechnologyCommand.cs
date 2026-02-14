namespace InterviewCoach.Application.Feature.Technology.Commands.CreateTechnology
{
    public sealed record CreateTechnologyCommand(string Title,string Slug,int DisplayOrder) : ICommand<Guid>;

}
