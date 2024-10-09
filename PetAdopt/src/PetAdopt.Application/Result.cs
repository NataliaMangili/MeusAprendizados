namespace PetAdopt.Application;

public class Result
{
    public bool Success { get; }
    public string Message { get; }
    public string ErrorCode { get; }

    private Result(bool success, string message = null, string errorCode = null)
    {
        Success = success;
        Message = message;
        ErrorCode = errorCode;
    }

    public static Result SuccessResult() => new Result(true);

    public static Result Failure(string message, string errorCode = null) => new Result(false, message, errorCode);
}