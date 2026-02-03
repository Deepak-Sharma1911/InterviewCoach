using System.Diagnostics.CodeAnalysis;

namespace InterviewCoach.Application.Wrappers
{
    public class Result
    {
        public Result(bool isSuccess, ApplicationError error)
        {
            if ((isSuccess && error != ApplicationError.None) || (!isSuccess && error == ApplicationError.None))
            {
                throw new ArgumentException("Invalid Error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public ApplicationError Error { get; set; }
        public static Result Success() => new Result(true, ApplicationError.None);
        public static Result<TValue> Success<TValue>(TValue value) =>
            new Result<TValue>(value, true, ApplicationError.None);
        public static Result Failure(ApplicationError error) => new Result(false, error);
        public static Result<TValue> Failure<TValue>(ApplicationError error) =>
            new Result<TValue>(default!, false, error);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        public Result(TValue? value, bool isSuccess, ApplicationError error) : base(isSuccess, error)
        {
            _value = value;
        }

        [NotNull]
        public TValue Value => IsSuccess
       ? _value!
       : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        public static implicit operator Result<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>(ApplicationError.NullValue);

        public static Result<TValue> ValidationFailure(ApplicationError error) =>
            new(default, false, error);
    }
}
