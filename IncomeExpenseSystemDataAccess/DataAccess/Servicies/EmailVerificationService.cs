using IncomeExpenseSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemDataAccess.DataAccess.Servicies;

public class EmailVerificationService
{
    private readonly IncomeExpenseSystemDbContext _dbContext;

    public EmailVerificationService(IncomeExpenseSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmailVerification> CreateEmailVerification(EmailVerification emailVerification)
    {
        _dbContext.Add(emailVerification);
        await _dbContext.SaveChangesAsync();
        
        return emailVerification;
    }

    public async Task<EmailVerification> GetEmailVerificationById(Guid emailVerificationId)
    {
        var emailVerification = await _dbContext.EmailVerifications.FindAsync(emailVerificationId);
        
        return emailVerification;
    }

    public async Task<List<EmailVerification>> GetEmailVerificationsByUserId(Guid userId)
    {
        var emailVerifications = await _dbContext.EmailVerifications
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        return emailVerifications;
    }
    
    public async Task<EmailVerification> UpdateEmailVerification(EmailVerification emailVerification)
    {
        _dbContext.Update(emailVerification);
        await _dbContext.SaveChangesAsync();
        
        return emailVerification;
    }

    public async Task DeleteEmailVerification(EmailVerification emailVerification) 
    {
        _dbContext.Remove(emailVerification);
        await _dbContext.SaveChangesAsync();   
    }
}