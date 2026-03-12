namespace IncomeExpenseSystemDataAccess.Entities;

public class EmailVerification
{
    public Guid EmailVerificationId { get; set; }
    public string EmailVerificationToken { get; set; }
    public DateTime EmailVerificationExpiresAt { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public EmailVerification() {}
    
    public EmailVerification(string emailVerificationToken, DateTime emailVerificationExpiresAt, Guid userId)
    {
        EmailVerificationToken = emailVerificationToken;
        EmailVerificationExpiresAt = emailVerificationExpiresAt;
        UserId = userId;
    }
    
    public EmailVerification(Guid emailVerificationId, string emailVerificationToken, DateTime emailVerificationExpiresAt, DateTime? emailVerifiedAt, Guid userId)
    {
        EmailVerificationId = emailVerificationId;
        EmailVerificationToken = emailVerificationToken;
        EmailVerificationExpiresAt = emailVerificationExpiresAt;
        EmailVerifiedAt = emailVerifiedAt;
        UserId = userId;
    }
}