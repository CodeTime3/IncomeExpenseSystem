using IncomeExpenseSystemDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemDataAccess.DataAccess.Servicies;

public class CategoryService
{
    private readonly IncomeExpenseSystemDbContext _dbContext;
    
    public CategoryService(IncomeExpenseSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> CreateCategory(Category category)
    {
        _dbContext.Add(category);
        await _dbContext.SaveChangesAsync();
        
        return category;
    }

    public async Task<Category> GetCategoryById(Guid categoryId)
    {
        var category = await _dbContext.Categories.FindAsync(categoryId);
        
        return category;
    }
    
    public async Task<List<Category>> GetCategoriesByUserId(Guid userId)
    {
        var categories = await _dbContext.Categories
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return categories;
    }
    
    public async Task<Category> UpdateCategory(Category category)
    {
        _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
        
        return category;
    }

    public async Task DeleteCategory(Category category)
    {//TODO: vedere per i log

        _dbContext.Remove(category);
        await _dbContext.SaveChangesAsync();
    }
}