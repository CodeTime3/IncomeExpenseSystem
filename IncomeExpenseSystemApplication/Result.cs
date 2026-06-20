namespace IncomeExpenseSystemApplication;

public class Result
{
    public bool IsSuccess { get; private set; }
    public Error? Error { get; private set; }

    public Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result OnSuccess() => new (true, null);
    
    public static Result OnFailure(Error error) => new (false, error);
}

public class Result<T> : Result
{
    public Result(bool isSuccess, Error? error, T value) : base(isSuccess, error)
    {
        Value = value;
    }

    public T? Value { get; private set; }
    
    public static Result<T> OnSuccess(T value) => new (true, null, value);
    
    public new static Result<T> OnFailure(Error error) => new (false, error, default);

    public Result<TResult> MapSuccess<TResult>(Func<T, Result<TResult>> map)
    {
        if (IsSuccess)
        {
            return map(Value);
        }

        return Result<TResult>.OnFailure(Error);
    }
}