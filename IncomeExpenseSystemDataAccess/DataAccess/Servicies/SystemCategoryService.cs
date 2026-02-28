using IncomeExpenseSystemDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemDataAccess.DataAccess.Servicies;

public class SystemCategoryService
{
    private readonly IncomeExpenseSystemDbContext _dbContext;
    
    public SystemCategoryService(IncomeExpenseSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SystemCategory> CreateSystemCategory(SystemCategory systemCategory)
    {
        _dbContext.Add(systemCategory);
        await _dbContext.SaveChangesAsync();
        
        return systemCategory;
    }

    public async Task<SystemCategory[]> GetAllSystemCategory()
    {
        var systemCategories = await _dbContext.SystemCategories.ToArrayAsync();
        
        return systemCategories;
    }
    
    public async Task<SystemCategory> GetSystemCategoryById(Guid id)
    {
        var systemCategory = await _dbContext.SystemCategories.FindAsync(id);
        
        return systemCategory;
    }
    
    public async Task<SystemCategory> UpdateSystemCategory(SystemCategory systemCategory)
    {
        _dbContext.Update(systemCategory);
        await _dbContext.SaveChangesAsync();
        
        return systemCategory;
    }
    
    public async Task DeleteSystemCategory(SystemCategory systemCategory)
    {
        _dbContext.Remove(systemCategory);
        await _dbContext.SaveChangesAsync();
    }
}