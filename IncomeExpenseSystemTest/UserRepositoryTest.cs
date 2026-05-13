using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDataAccess.Repositories;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class UserRepositoryTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private User user;
    private IUserRepository userRepository;
    
    [Fact]
    public async Task CreateUser_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        user = new User
        (
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38"), 
            "daniele.pilloni@gmail.com", 
            "Password",
            null,
            DateTime.Now
        );
        userRepository = new UserRepository(context);
        
        var userDb = await userRepository.CreateUser(user);
        
        Assert.Equal("daniele.pilloni@gmail.com", userDb.UserMail);
        Assert.Equal("Password", userDb.UserPassword);
    }
    
    [Fact]
    public async Task GetAllUsers_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        userRepository = new UserRepository(context);
        
        var users = await userRepository.GetAllUsers();
        
        Assert.NotNull(users);
        Assert.Single(users);
    }
    
    [Fact]
    public async Task GetUserById_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        userRepository = new UserRepository(context);

        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var userDb = await userRepository.GetUserById(userId);
        
        Assert.Equal("daniele.pilloni@gmail.com", userDb.UserMail);
        Assert.Equal("Password", userDb.UserPassword);
    }

    [Fact]
    public async Task UpdateUser_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        userRepository = new UserRepository(context);
        user = new User
        (
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38"), 
            "daniele.pilloni@gmail.com", 
            "password",
            DateTime.Now,
            DateTime.Now
        );
        
        var userDb = await userRepository.UpdateUser(user);
        
        Assert.Equal("daniele.pilloni@gmail.com", userDb.UserMail);
        Assert.Equal("password", userDb.UserPassword);
    }

    [Fact]
    public async Task DeleteUser_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        userRepository = new UserRepository(context);
        
        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var userDb = await userRepository.GetUserById(userId);
        
        await userRepository.DeleteUser(userDb);
        
        var users = await context.Users.ToArrayAsync();
        
        Assert.Empty(users);
    }
}