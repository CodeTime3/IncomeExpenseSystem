using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDataAccess.Repositories;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Models;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class TransactionRepositoryTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private Transaction transaction;
    private ITransactionRepository _transactionRepository ;
    
    [Fact]
    public async Task CreateTransaction_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        transaction = new Transaction
        (
            Guid.Parse("939b9ad3-0e4a-4199-aa35-f45babf68dba"),
            10,
            "Test",
            DateTime.Now,
            TransactionType.Expense,
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38"),
            Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba"),
            Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba")
        );
        _transactionRepository = new TransactionRepository(context);
        
        var transactionDb = await _transactionRepository.CreateTransaction(transaction);
        
        Assert.Equal(10, transactionDb.TransactionAmount);
        Assert.Equal("Test", transactionDb.TransactionDescription);
        Assert.Equal(TransactionType.Expense, transactionDb.TransactionType);
    }
    
    [Fact]
    public async Task GetTransactionByTransactionId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _transactionRepository = new TransactionRepository(context);

        var transactionId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f45babf68dba");
        var transactionDb = await _transactionRepository.GetTransactionByTransactionId(transactionId);
        
        Assert.NotNull(transactionDb);
    }
    
    [Fact]
    public async Task GetTransactionsByUserId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _transactionRepository = new TransactionRepository(context);

        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var transactions = await _transactionRepository.GetTransactionsByUserId(userId);
        
        Assert.NotNull(transactions);
        Assert.Single(transactions);
    }

    [Fact]
    public async Task UpdateTransaction_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _transactionRepository = new TransactionRepository(context);
        transaction = new Transaction
        (
            Guid.Parse("939b9ad3-0e4a-4199-aa35-f45babf68dba"),
            15,
            "test",
            DateTime.Now,
            TransactionType.Expense,
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38"),
            Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba"),
            Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba")
        );
        
        var transactionDb = await _transactionRepository.UpdateTransaction(transaction);
        
        Assert.Equal(15, transactionDb.TransactionAmount);
        Assert.Equal("test", transactionDb.TransactionDescription);
        Assert.Equal(TransactionType.Expense, transactionDb.TransactionType);
    }

    [Fact]
    public async Task DeleteTransaction_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _transactionRepository = new TransactionRepository(context);
        
        var transactionId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f45babf68dba");
        var transactionDb = await _transactionRepository.GetTransactionByTransactionId(transactionId);
        
        await _transactionRepository.DeleteTransaction(transactionDb);
        
        var transactions = await context.Transactions.ToArrayAsync();
        
        Assert.Empty(transactions);
    }
}