using IncomeExpenseSystemDomain.Entities;

namespace IncomeExpenseSystemDomain.Repositories;

public interface ISystemCategoryRepository
{
    Task<SystemCategory> CreateSystemCategory(SystemCategory systemCategory);
    Task<SystemCategory[]> GetAllSystemCategory();
    Task<SystemCategory> GetSystemCategoryById(Guid id);
    Task<SystemCategory> UpdateSystemCategory(SystemCategory systemCategory);
    Task DeleteSystemCategory(SystemCategory systemCategory);
}