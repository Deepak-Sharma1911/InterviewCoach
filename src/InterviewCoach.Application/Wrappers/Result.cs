namespace InterviewCoach.Application.Wrappers
{
    public class Result
    {
        public Result(bool isSucess, ApplicationError error)
        {
            if (isSucess && error != ApplicationError.None || !isSucess && error == ApplicationError.None)
                IsSucess = isSucess;
        }
        public bool IsSucess { get; }
        public bool IsFailure => !IsSucess;

    }
}
