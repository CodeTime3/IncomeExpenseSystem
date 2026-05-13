using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemDataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IncomeExpenseSystemDbContext _dbContext;

    public UserRepository(IncomeExpenseSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUser(User user)
    {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
        
        return user;
    }

    public async Task<User[]> GetAllUsers()
    {
        var categories = await _dbContext.Users.ToArrayAsync();
        
        return categories;
    }

    public async Task<User> GetUserById(Guid userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }
    
    public async Task<User> UpdateUser(User user)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
        
        return user;
    }

    public async Task DeleteUser(User user)
    {
        
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();   
    }
}