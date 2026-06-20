using IncomeExpenseSystemApplication;
using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDataAccess.Repositories;
using IncomeExpenseSystemDomain;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Models;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;


namespace IncomeExpenseSystemTest;

public class AccountServiceTests
{
    public static TheoryData<string, DateTime, bool> VerifyEmailTestData =>
        new()
        {
            { 
                Guid.NewGuid().ToString(), 
                DateTime.Now.AddHours(2), 
                true
            },
            
            { 
                Guid.NewGuid().ToString(), 
                DateTime.Now, 
                false
            },
        };
    
    [Theory]
    [MemberData(nameof(VerifyEmailTestData))]
    public async Task VerifyEmail_should_work(string token, DateTime expireDate, bool expected)
    {
        var emailVerificationRepository = Substitute.For<IEmailVerificationRepository>();
        var userRepository = Substitute.For<IUserRepository>();
        var jwtService = Substitute.For<IJwtService>();
        var accountService = new AccountService(userRepository, emailVerificationRepository, jwtService, null, null, null);

        emailVerificationRepository.GetEmailVerificationByToken(token).Returns(Task.FromResult(new EmailVerification(Guid.NewGuid(), token, expireDate, null, Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38"))));
        userRepository.GetUserById(Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")).Returns(Task.FromResult(new User(Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38"), "daniele.pilloni@gmail.com", "Password", null, DateTime.Now)));
        jwtService.CreateJwt(Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")).Returns("jwt created");
        
        var result = await accountService.VerifyEmail(token);
        
        Assert.Equal(expected, result.IsSuccess);
    }
}