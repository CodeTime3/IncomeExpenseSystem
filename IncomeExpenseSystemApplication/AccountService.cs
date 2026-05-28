using IncomeExpenseSystemDomain;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Models;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IncomeExpenseSystemApplication;

public class AccountService
{
    private PasswordHasher<User> _pwdHasher = new();
    private IUserRepository _userRepository;
    private IEmailVerificationRepository _emailVerificationRepository;
    private JwtService _jwtService;
    private JwtModel _jwtModel;
    private SendEmailConfirmService _sendEmailConfirmService;
    private EmailModel _emailModel;
    private IMyDbContext _dbContext;
    
    public AccountService(IUserRepository userRepository, IEmailVerificationRepository emailVerificationRepository, 
        JwtService jwtService, JwtModel jwtModel, SendEmailConfirmService sendEmailConfirmService, 
        EmailModel emailModel, IMyDbContext dbContext)
    {
        _userRepository = userRepository;
        _emailVerificationRepository = emailVerificationRepository;
        _jwtService = jwtService;
        _jwtModel = jwtModel;
        _sendEmailConfirmService = sendEmailConfirmService;
        _emailModel = emailModel;
        _dbContext = dbContext;
    }

    public async Task<Result> SignUp(AuthModel authModel)
    {
        var user = await _userRepository.GetUserByMail(authModel.Mail);

        if (user is not null)
        {
            return Result.OnFailure("That email is already used");
        }
        
        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            UserMail = authModel.Mail,
            UserCreatedAt = DateTime.Now
        };
        
        newUser.UserPassword = _pwdHasher.HashPassword(newUser, authModel.Password);
        using var transaction = await _dbContext.BeginTransactionAsync();
        try
        {
            var createdUser = await _userRepository.CreateUser(newUser);
            var token = Guid.NewGuid().ToString();
            var email = new EmailVerification
            {
                EmailVerificationId = Guid.NewGuid(),
                EmailVerificationToken = token,
                EmailVerificationExpiresAt = DateTime.Now.AddHours(2),
                UserId = createdUser.UserId
            };
            await _emailVerificationRepository.CreateEmailVerification(email);
            _sendEmailConfirmService.SendEmail(_emailModel, createdUser.UserMail, token);

            await transaction.CommitAsync();
            return Result.OnSuccess("Check your email");
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Result.OnFailure(e.Message);
        }
    }

    public async Task<Result<string>> VerifyEmail(string token)
    {
        if (token.IsNullOrEmpty())
        {
            return Result<string>.OnFailure("Token not found", string.Empty);
        }

        var emailVerification = await _emailVerificationRepository.GetEmailVerificationByToken(token);

        if (emailVerification is null)
        {
            return Result<string>.OnFailure("Email not found", string.Empty);
        }

        if (emailVerification.EmailVerificationExpiresAt >= DateTime.Now)
        {
            return Result<string>.OnFailure("Token expired. If you signed up before click here to receive a new verify email", string.Empty);
        }

        if (emailVerification.EmailVerifiedAt is not null)
        {
            return Result<string>.OnFailure("Email already verified", string.Empty);
        }

        emailVerification.EmailVerifiedAt = DateTime.Now;
        await _emailVerificationRepository.UpdateEmailVerification(emailVerification);
        
        var user = await _userRepository.GetUserById(emailVerification.UserId);
        user.UserMailVerifiedAt = DateTime.Now;
        await _userRepository.UpdateUser(user);
        
        var jwt = _jwtService.CreateJwt(_jwtModel, user.UserId);
        
        return Result<string>.OnSuccess(jwt, "Email verified");
    }

    public async Task<Result> ResendEmail(AuthModel authModel)
    {
        var user = await _userRepository.GetUserByMail(authModel.Mail);

        if (user is null)
        {
            return Result.OnFailure("Email not found");
        }

        if (user.UserMailVerifiedAt is not null)
        {
            return Result.OnFailure("Email already verified");
        }
        
        var token = Guid.NewGuid().ToString();
        var email = new EmailVerification
        {
            EmailVerificationId = Guid.NewGuid(),
            EmailVerificationToken = token,
            EmailVerificationExpiresAt = DateTime.Now.AddHours(2),
            UserId = user.UserId
        };
        await _emailVerificationRepository.CreateEmailVerification(email);
        _sendEmailConfirmService.SendEmail(_emailModel, user.UserMail, token);
        
        return Result.OnSuccess("Check your email");
    }

    public async Task<Result<string>> SignIn(AuthModel authModel)
    {
        var user = await _userRepository.GetUserByMail(authModel.Mail);

        if (user is null)
        {
            return Result<string>.OnFailure("Email not found", string.Empty);
        }

        if (user.UserMailVerifiedAt is null)
        {
            return Result<string>.OnFailure("Email isn't verified", string.Empty);
        }

        var hash = _pwdHasher.VerifyHashedPassword(user, user.UserPassword, authModel.Password);

        if (!user.UserMail.Equals(authModel.Mail) || hash is PasswordVerificationResult.Success)
        {
            return Result<string>.OnFailure("Incorrect email or password", string.Empty);
        }
        
        var jwt = _jwtService.CreateJwt(_jwtModel, user.UserId);
        
        return Result<string>.OnSuccess(jwt);
    }

    public async Task<Result> ResetPassword(ResetPasswordModel model)
    {
        var user = await _userRepository.GetUserByMail(model.Email);

        if (user is null)
        {
            return Result.OnFailure("Email not found");
        }
        
        if (user.UserMailVerifiedAt is null)
        {
            return Result.OnFailure("Email isn't verified");
        }

        user.UserPassword = _pwdHasher.HashPassword(user, model.NewPassword);
        await _userRepository.UpdateUser(user);
        
        return Result.OnSuccess("Check your email");
    }

    public async Task<Result> DeleteAccount(DeleteAccountModel model)
    {
        var user = await _userRepository.GetUserByMail(model.Email);
        
        if (user is null)
        {
            return Result.OnFailure("Email not found");
        }
        
        await _userRepository.DeleteUser(user);
        return Result.OnSuccess("Account deleted");
    }
}