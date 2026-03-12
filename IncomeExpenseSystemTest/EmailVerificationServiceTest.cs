using IncomeExpenseSystemDataAccess.DataAccess;
using IncomeExpenseSystemDataAccess.DataAccess.Servicies;
using IncomeExpenseSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class EmailVerificationServiceTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private EmailVerification emailVerification;
    private EmailVerificationService emailVerificationService;
    
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
        emailVerificationService = new EmailVerificationService(context);
        
        var emailVerificationDb = await emailVerificationService.CreateEmailVerification(emailVerification);
        
        Assert.Equal("532b2bfa-87ae-435a-b91b-76297442ac49", emailVerificationDb.EmailVerificationToken);
    }
    
    [Fact]
    public async Task GetEmailVerificationById_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        emailVerificationService = new EmailVerificationService(context);

        var emailVerificationId = Guid.Parse("532b2bfa-87ae-592b-b91b-76297442ac38");
        var emailVerificationDb = await emailVerificationService.GetEmailVerificationById(emailVerificationId);
        
        Assert.Equal("532b2bfa-87ae-435a-b91b-76297442ac49", emailVerificationDb.EmailVerificationToken);
    }
    
    [Fact]
    public async Task GetEmailVerificationsByUserId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        emailVerificationService = new EmailVerificationService(context);

        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var emailVerificationsDb = await emailVerificationService.GetEmailVerificationsByUserId(userId);
        
        Assert.Single(emailVerificationsDb);
    }

    [Fact]
    public async Task UpdateEmailVerification_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        emailVerificationService = new EmailVerificationService(context);
        emailVerification = new EmailVerification
        (
            Guid.Parse("532b2bfa-87ae-592b-b91b-76297442ac38"),
            "532b2bfa-87ae-435a-b91b-76297442ac49",
            DateTime.Now,
            DateTime.Now,
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")
        );
        
        var emailVerificationDb = await emailVerificationService.UpdateEmailVerification(emailVerification);
        
        Assert.Equal(DateTime.Now, emailVerificationDb.EmailVerificationExpiresAt);
    }

    [Fact]
    public async Task DeleteEmailVerification_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        emailVerificationService = new EmailVerificationService(context);
        
        var emailVerificationId = Guid.Parse("532b2bfa-87ae-592b-b91b-76297442ac38");
        var emailVerificationDb = await emailVerificationService.GetEmailVerificationById(emailVerificationId);
        
        await emailVerificationService.DeleteEmailVerification(emailVerificationDb);
        
        var emailVerifications = await context.EmailVerifications.ToArrayAsync();
        
        Assert.Empty(emailVerifications);
    }
}