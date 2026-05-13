using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDataAccess.Repositories;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class EmailVerificationRepositoryTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private EmailVerification emailVerification;
    private IEmailVerificationRepository _emailVerificationRepository;
    
    [Fact]
    public async Task CreateEmailVerification_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        emailVerification = new EmailVerification
        (
            Guid.Parse("532b2bfa-87ae-592b-b91b-76297442ac38"),
            "532b2bfa-87ae-435a-b91b-76297442ac49", 
            DateTime.Now,
            DateTime.Now,
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")
        );
        _emailVerificationRepository = new EmailVerificationRepository(context);
        
        var emailVerificationDb = await _emailVerificationRepository.CreateEmailVerification(emailVerification);
        
        Assert.Equal("532b2bfa-87ae-435a-b91b-76297442ac49", emailVerificationDb.EmailVerificationToken);
    }
    
    [Fact]
    public async Task GetEmailVerificationById_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _emailVerificationRepository = new EmailVerificationRepository(context);

        var emailVerificationId = Guid.Parse("532b2bfa-87ae-592b-b91b-76297442ac38");
        var emailVerificationDb = await _emailVerificationRepository.GetEmailVerificationById(emailVerificationId);
        
        Assert.Equal("532b2bfa-87ae-435a-b91b-76297442ac49", emailVerificationDb.EmailVerificationToken);
    }
    
    [Fact]
    public async Task GetEmailVerificationsByUserId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _emailVerificationRepository = new EmailVerificationRepository(context);

        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var emailVerificationsDb = await _emailVerificationRepository.GetEmailVerificationsByUserId(userId);
        
        Assert.Single(emailVerificationsDb);
    }

    [Fact]
    public async Task UpdateEmailVerification_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _emailVerificationRepository = new EmailVerificationRepository(context);
        emailVerification = new EmailVerification
        (
            Guid.Parse("532b2bfa-87ae-592b-b91b-76297442ac38"),
            "532b2bfa-87ae-435a-b91b-76297442ac49",
            DateTime.Now,
            DateTime.Now,
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")
        );
        
        var emailVerificationDb = await _emailVerificationRepository.UpdateEmailVerification(emailVerification);
        
        Assert.Equal(DateTime.Now, emailVerificationDb.EmailVerificationExpiresAt);
    }

    [Fact]
    public async Task DeleteEmailVerification_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _emailVerificationRepository = new EmailVerificationRepository(context);
        
        var emailVerificationId = Guid.Parse("532b2bfa-87ae-592b-b91b-76297442ac38");
        var emailVerificationDb = await _emailVerificationRepository.GetEmailVerificationById(emailVerificationId);
        
        await _emailVerificationRepository.DeleteEmailVerification(emailVerificationDb);
        
        var emailVerifications = await context.EmailVerifications.ToArrayAsync();
        
        Assert.Empty(emailVerifications);
    }
}