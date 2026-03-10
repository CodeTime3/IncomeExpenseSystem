using IncomeExpenseSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeExpenseSystemDataAccess.DataAccess.Configuration;

public class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
{
    public void Configure(EntityTypeBuilder<EmailVerification> builder)
    {
        builder.ToTable("EmailVerifications");

        builder.HasKey(x => x.EmailVerificationId);
        
        builder.Property(x => x.EmailVerificationToken)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.EmailVerificationExpiresAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.EmailVerifiedAt)
            .HasColumnType("datetime");
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}