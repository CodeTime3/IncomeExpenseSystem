using IncomeExpenseSystemDomain.Entities;

namespace IncomeExpenseSystemDomain.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> CreateTransaction(Transaction transaction);
    Task<Transaction> GetTransactionByTransactionId(Guid id);
    Task<List<Transaction>> GetTransactionsByUserId(Guid userId);
    Task<Transaction> UpdateTransaction(Transaction transaction);
    Task DeleteTransaction(Transaction transaction);
}