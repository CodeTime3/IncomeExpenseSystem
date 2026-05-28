using System.Runtime.CompilerServices;

namespace IncomeExpenseSystemApplication;

public class Result
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    
    public static Result OnSuccess(string successMessage = "") => new (true, successMessage);
    
    public static Result OnFailure(string errorMessage) => new (false, errorMessage);
}

public class Result<T> : Result
{
    public Result(bool isSuccess, string message, T value) : base(isSuccess, message)
    {
        Value = value;
    }

    public T? Value { get; private set; }
    
    public new static Result<T> OnSuccess(T value, string successMessage = "") => new (true, successMessage, value);
    
    public new static Result<T> OnFailure(string errorMessage, T value) => new (true, errorMessage, value);
}