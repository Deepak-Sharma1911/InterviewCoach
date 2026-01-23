namespace InterviewCoach.Application.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public IReadOnlyCollection<ValidationError> Errors { get; }

        public ValidationException(IEnumerable<FluentValidation.Results.ValidationFailure> failures)
            : base("One or more validation errors occurred.")
        {
            Errors = failures
                .Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
                .ToArray();
        }
    }

    public sealed record ValidationError(string Property, string Message);
}
