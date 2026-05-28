namespace IncomeExpenseSystemDomain;

public interface IMyDbContextTransaction : IDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}