namespace InterviewCoach.Application.Wrappers
{
    public record ApplicationError
    {
        public static readonly ApplicationError None = new ApplicationError(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly ApplicationError NullValue = new ApplicationError("NULL_VALUE", "The value is null.", ErrorType.Failure);
        public ApplicationError(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }
        public static ApplicationError Failure(string code, string description) => new ApplicationError(code, description, ErrorType.Failure);
        public static ApplicationError Conflict(string code, string description) => new ApplicationError(code, description, ErrorType.Conflict);
        public static ApplicationError NotFound(string code, string description) => new ApplicationError(code, description, ErrorType.NotFound);
        public static ApplicationError Problem(string code, string description) => new ApplicationError(code, description, ErrorType.Problem);
    }
}
