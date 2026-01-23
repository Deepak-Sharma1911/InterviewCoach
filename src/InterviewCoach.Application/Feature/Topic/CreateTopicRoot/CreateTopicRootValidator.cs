using FluentValidation;

namespace InterviewCoach.Application.Feature.Topic.CreateTopicRoot
{
    public class CreateTopicRootValidator : AbstractValidator<CreateTopicCommand>
    {
        public CreateTopicRootValidator(CreateTopicCommand topicCommand)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .MaximumLength(100).WithMessage("Slug must not exceed 100 characters.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be URL friendly (lowercase letters, numbers, and hyphens only).");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Display order must be a non-negative integer.");
        }
    }
}
