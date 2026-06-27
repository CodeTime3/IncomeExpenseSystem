namespace IncomeExpenseSystemApplication;

public class Result
{
    protected bool IsSuccess { get; private set; }
    public Error? Error { get; private set; }

    public Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    public Result()
    {
        IsSuccess = true;
    }
}

public class Result<T> : Result
{
    public T? Value { get; private set; }
    
    public Result(T value) : base()
    {
        Value = value;
    }
    
    public Result(Error error) : base(error)
    {
        Value = default(T);
    }

    public Result<TValue> MapSuccess<TValue>(Func<T, TValue> map)
    {
        return IsSuccess ? new Result<TValue>(map(Value)) : new Result<TValue>(Error);
    }

    public TReturn Match<TReturn>(Func<T, TReturn> onSuccess, Func<Error, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Error);
    }
}