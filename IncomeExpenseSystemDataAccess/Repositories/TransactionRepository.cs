using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemDataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IncomeExpenseSystemDbContext _dbContext;
    
    public TransactionRepository(IncomeExpenseSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Transaction> CreateTransaction(Transaction transaction)
    {
        _dbContext.Add(transaction);
        await _dbContext.SaveChangesAsync();
        
        return transaction;
    }

    public async Task<Transaction> GetTransactionByTransactionId(Guid id)
    {
        var transaction = await _dbContext.Transactions.FindAsync(id);
        
        return transaction;
    }

    public async Task<List<Transaction>> GetTransactionsByUserId(Guid userId)
    {
        var transactions = await _dbContext.Transactions
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        return transactions;
    }
    
    public async Task<Transaction> UpdateTransaction(Transaction transaction)
    {
        _dbContext.Update(transaction);
        await _dbContext.SaveChangesAsync();
        
        return transaction;
    }
    
    public async Task DeleteTransaction(Transaction transaction)
    {
        _dbContext.Remove(transaction);
        await _dbContext.SaveChangesAsync();
    }
}