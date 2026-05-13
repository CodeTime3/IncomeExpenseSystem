using IncomeExpenseSystemDomain.Entities;

namespace IncomeExpenseSystemDomain.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateCategory(Category category);
    Task<Category> GetCategoryById(Guid categoryId);
    Task<List<Category>> GetCategoriesByUserId(Guid userId);
    Task<Category> UpdateCategory(Category category);
    Task DeleteCategory(Category category);
}