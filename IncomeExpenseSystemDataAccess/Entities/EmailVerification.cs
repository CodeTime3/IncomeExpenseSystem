namespace IncomeExpenseSystemDataAccess.Entities;

public class EmailVerification
{
    public Guid EmailVerificationId { get; set; }
    public string EmailVerificationToken { get; set; }
    public DateTime EmailVerificationExpiresAt { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}