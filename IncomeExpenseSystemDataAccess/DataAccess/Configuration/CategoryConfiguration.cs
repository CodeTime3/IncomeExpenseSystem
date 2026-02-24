using IncomeExpenseSystemDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeExpenseSystemDataAccess.DataAccess.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        
        builder.HasKey(c => c.CategoryId);
        
        builder.Property(e => e.CategoryName)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasMany(c => c.Transactions)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.User)
            .WithMany(e => e.Categories)
            .HasForeignKey(e => e.UserId);
    }
}