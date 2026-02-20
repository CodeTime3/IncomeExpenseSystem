using IncomeExpenseSystemDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeExpenseSystemDataAccess.DataAccess.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        
        builder.HasKey(e => e.TransactionId);
        
        builder.Property(e => e.TransactionAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(e => e.TransactionDescription)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(e => e.TransactionDate)
            .HasColumnType("datetime")
            .IsRequired();
        
        builder.Property(e => e.TransactionType)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Category)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.CategoryId);
    }
}