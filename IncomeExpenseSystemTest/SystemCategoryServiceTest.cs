using IncomeExpenseSystemDataAccess.DataAccess;
using IncomeExpenseSystemDataAccess.DataAccess.Servicies;
using IncomeExpenseSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class SystemCategoryServiceTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private SystemCategory systemCategory;
    private SystemCategoryService systemCategoryService;
    
    [Fact]
    public async Task CreateSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        systemCategory = new SystemCategory
        (
            Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba"), 
            "Test"
        );
        systemCategoryService = new SystemCategoryService(context);
        
        var systemCategoryDb = await systemCategoryService.CreateSystemCategory(systemCategory);
        
        Assert.Equal("Test", systemCategoryDb.SystemCategoryName);
    }
    
    [Fact]
    public async Task GetAllSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        systemCategoryService = new SystemCategoryService(context);
        
        var systemCategories = await systemCategoryService.GetAllSystemCategory();
        
        Assert.NotNull(systemCategories);
        Assert.Single(systemCategories);
    }
    
    [Fact]
    public async Task GetSystemCategoryById_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        systemCategoryService = new SystemCategoryService(context);

        var systemCategoryId = Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba");
        var systemCategoryDb = await systemCategoryService.GetSystemCategoryById(systemCategoryId);
        
        Assert.Equal("Test", systemCategoryDb.SystemCategoryName);
    }

    [Fact]
    public async Task UpdateSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        systemCategoryService = new SystemCategoryService(context);
        systemCategory = new SystemCategory
        (
            Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba"), 
            "test"
        );
        
        var systemCategoryDb = await systemCategoryService.UpdateSystemCategory(systemCategory);
        
        Assert.Equal("test", systemCategoryDb.SystemCategoryName);
    }

    [Fact]
    public async Task DeleteSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        systemCategoryService = new SystemCategoryService(context);
        
        var systemCategoryId = Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba");
        var systemCategoryDb = await systemCategoryService.GetSystemCategoryById(systemCategoryId);
        
        await systemCategoryService.DeleteSystemCategory(systemCategoryDb);
        
        var systemCategories = await context.SystemCategories.ToArrayAsync();
        
        Assert.Empty(systemCategories);
    }
}