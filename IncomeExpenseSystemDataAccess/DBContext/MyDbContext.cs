using IncomeExpenseSystemDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IncomeExpenseSystemDataAccess.DBContext;

public class MyDbContext : IMyDbContext
{
    IncomeExpenseSystemDbContext _dbContext;

    public MyDbContext(IncomeExpenseSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IMyDbContextTransaction> BeginTransactionAsync()
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        return new MyDbContextTransaction(transaction);
    }
}