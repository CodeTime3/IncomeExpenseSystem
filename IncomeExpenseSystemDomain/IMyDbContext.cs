namespace IncomeExpenseSystemDomain;

public interface IMyDbContext
{
    Task<IMyDbContextTransaction> BeginTransactionAsync();
}