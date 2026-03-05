using IncomeExpenseSystemDataAccess;
using IncomeExpenseSystemDataAccess.DataAccess;
using IncomeExpenseSystemDataAccess.DataAccess.Servicies;
using IncomeExpenseSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class TransactionServiceTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private Transaction transaction;
    private TransactionService transactionService ;
    
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
        transactionService = new TransactionService(context);
        
        var transactionDb = await transactionService.CreateTransaction(transaction);
        
        Assert.Equal(10, transactionDb.TransactionAmount);
        Assert.Equal("Test", transactionDb.TransactionDescription);
        Assert.Equal(TransactionType.Expense, transactionDb.TransactionType);
    }
    
    [Fact]
    public async Task GetTransactionByTransactionId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        transactionService = new TransactionService(context);

        var transactionId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f45babf68dba");
        var transactionDb = await transactionService.GetTransactionByTransactionId(transactionId);
        
        Assert.NotNull(transactionDb);
    }
    
    [Fact]
    public async Task GetTransactionsByUserId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        transactionService = new TransactionService(context);

        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var transactions = await transactionService.GetTransactionsByUserId(userId);
        
        Assert.NotNull(transactions);
        Assert.Single(transactions);
    }

    [Fact]
    public async Task UpdateTransaction_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        transactionService = new TransactionService(context);
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
        
        var transactionDb = await transactionService.UpdateTransaction(transaction);
        
        Assert.Equal(15, transactionDb.TransactionAmount);
        Assert.Equal("test", transactionDb.TransactionDescription);
        Assert.Equal(TransactionType.Expense, transactionDb.TransactionType);
    }

    [Fact]
    public async Task DeleteTransaction_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        transactionService = new TransactionService(context);
        
        var transactionId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f45babf68dba");
        var transactionDb = await transactionService.GetTransactionByTransactionId(transactionId);
        
        await transactionService.DeleteTransaction(transactionDb);
        
        var transactions = await context.Transactions.ToArrayAsync();
        
        Assert.Empty(transactions);
    }
}