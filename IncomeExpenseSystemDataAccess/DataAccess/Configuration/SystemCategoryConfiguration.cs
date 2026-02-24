using IncomeExpenseSystemDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeExpenseSystemDataAccess.DataAccess.Configuration;

public class SystemCategoryConfiguration : IEntityTypeConfiguration<SystemCategory>
{
    public void Configure(EntityTypeBuilder<SystemCategory> builder)
    {
        builder.ToTable("SystemCategories");
        
        builder.HasKey(s => s.SystemCategoryId);
        
        builder.Property(s => s.SystemCategoryName)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasMany(s => s.Transactions)
            .WithOne(s => s.SystemCategory)
            .HasForeignKey(s => s.SystemCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}