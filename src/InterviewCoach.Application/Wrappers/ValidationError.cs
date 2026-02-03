namespace InterviewCoach.Application.Wrappers
{
    public sealed record ValidationError : ApplicationError
    {
        public ValidationError(ApplicationError[] errors) : base(
            "Validation.General",
            "One or more validation errors occurred",
            ErrorType.Validation)
        {
            Errors = errors;
        }

        public ApplicationError[] Errors { get; set; }

        public static ValidationError FromResults(IEnumerable<Result> results) => new ValidationError(results.Where(x => x.IsFailure).Select(x => x.Error).ToArray());
    }
}
