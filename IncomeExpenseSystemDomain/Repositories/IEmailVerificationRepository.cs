using IncomeExpenseSystemDomain.Entities;

namespace IncomeExpenseSystemDomain.Repositories;

public interface IEmailVerificationRepository
{
    Task<EmailVerification> CreateEmailVerification(EmailVerification emailVerification);
    Task<EmailVerification> GetEmailVerificationById(Guid emailVerificationId);
    Task<List<EmailVerification>> GetEmailVerificationsByUserId(Guid userId);
    Task<EmailVerification> UpdateEmailVerification(EmailVerification emailVerification);
    Task DeleteEmailVerification(EmailVerification emailVerification);
}