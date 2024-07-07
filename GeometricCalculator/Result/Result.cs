namespace GeometricCalculator.Result;

public class Result<T>
{
    public T? Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public ErrorCode? ErrorCode { get; private set; }

    private Result(T? value, ErrorCode? errorCode = null)
    {
        Value = value;
        ErrorCode = errorCode;
        IsSuccess = errorCode == null;
    }

    public static Result<T> Ok(T value)
    {
        return new Result<T>(value);
    }

    public static Result<T> Error(ErrorCode errorCode, string? message = null)
    {
        return new Result<T>(default, errorCode);
    }
}