using IncomeExpenseSystemDomain.Entities;

namespace IncomeExpenseSystemDomain.Repositories;

public interface IEmailVerificationRepository
{
    Task<EmailVerification> CreateEmailVerification(EmailVerification emailVerification);
    Task<EmailVerification?> GetEmailVerificationByToken(string token);
    Task<List<EmailVerification>> GetEmailVerificationsByUserId(Guid userId);
    Task<EmailVerification> UpdateEmailVerification(EmailVerification emailVerification);
    Task DeleteEmailVerification(EmailVerification emailVerification);
}