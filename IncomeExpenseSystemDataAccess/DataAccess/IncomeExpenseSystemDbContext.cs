using IncomeExpenseSystemDataAccess.DataAccess.Configuration;
using IncomeExpenseSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemDataAccess.DataAccess;

public class IncomeExpenseSystemDbContext : DbContext
{
    public IncomeExpenseSystemDbContext(DbContextOptions<IncomeExpenseSystemDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SystemCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new EmailVerificationConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SystemCategory> SystemCategories { get; set; }
    public  DbSet<EmailVerification> EmailVerifications { get; set; }
}