using IncomeExpenseSystemDomain;
using Microsoft.EntityFrameworkCore.Storage;

namespace IncomeExpenseSystemDataAccess;

public class MyDbContextTransaction : IMyDbContextTransaction
{
    IDbContextTransaction _dbContextTransaction;

    public MyDbContextTransaction(IDbContextTransaction dbContextTransaction)
    {
        _dbContextTransaction = dbContextTransaction;
    }
    
    public async Task CommitAsync()
    {
        await _dbContextTransaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _dbContextTransaction.RollbackAsync();
    }

    public void Dispose()
    {
        _dbContextTransaction.Dispose();
    }
}