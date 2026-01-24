using IncomeExpenseSystemDataAccess.DataAccess.Configuration;
using IncomeExpenseSystemDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemDataAccess.DataAccess;

public class IncomeExpenseSystemDbContext : DbContext
{
    public IncomeExpenseSystemDbContext(DbContextOptions<IncomeExpenseSystemDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Expense> Expenses { get; set; }
}