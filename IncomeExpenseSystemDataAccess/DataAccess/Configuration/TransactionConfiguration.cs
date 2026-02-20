using IncomeExpenseSystemDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeExpenseSystemDataAccess.DataAccess.Configuration;

public class ExpenseConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Expenses");
        
        builder.Property(e => e.AmountExpense)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(e => e.DescriptionExpense)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(e => e.DateExpense)
            .HasColumnType("datetime")
            .IsRequired();
    }
}