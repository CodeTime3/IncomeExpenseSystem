using IncomeExpenseSystemDomain.Entities;

namespace IncomeExpenseSystemDomain.Repositories;

public interface IUserRepository
{
    Task<User> CreateUser(User user);
    Task<User?> GetUserById(Guid userId);
    Task<User?> GetUserByMail(string mail);
    Task<User> UpdateUser(User user);
    Task DeleteUser(User user);
}