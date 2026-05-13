using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDataAccess.Repositories;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class SystemCategoryRepositoryTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private SystemCategory systemCategory;
    private ISystemCategoryRepository _systemCategoryRepository;
    
    [Fact]
    public async Task CreateSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        systemCategory = new SystemCategory
        (
            Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba"), 
            "Test"
        );
        _systemCategoryRepository = new SystemCategoryRepository(context);
        
        var systemCategoryDb = await _systemCategoryRepository.CreateSystemCategory(systemCategory);
        
        Assert.Equal("Test", systemCategoryDb.SystemCategoryName);
    }
    
    [Fact]
    public async Task GetAllSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _systemCategoryRepository = new SystemCategoryRepository(context);
        
        var systemCategories = await _systemCategoryRepository.GetAllSystemCategory();
        
        Assert.NotNull(systemCategories);
        Assert.Single(systemCategories);
    }
    
    [Fact]
    public async Task GetSystemCategoryById_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _systemCategoryRepository = new SystemCategoryRepository(context);

        var systemCategoryId = Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba");
        var systemCategoryDb = await _systemCategoryRepository.GetSystemCategoryById(systemCategoryId);
        
        Assert.Equal("Test", systemCategoryDb.SystemCategoryName);
    }

    [Fact]
    public async Task UpdateSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _systemCategoryRepository = new SystemCategoryRepository(context);
        systemCategory = new SystemCategory
        (
            Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba"), 
            "test"
        );
        
        var systemCategoryDb = await _systemCategoryRepository.UpdateSystemCategory(systemCategory);
        
        Assert.Equal("test", systemCategoryDb.SystemCategoryName);
    }

    [Fact]
    public async Task DeleteSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _systemCategoryRepository = new SystemCategoryRepository(context);
        
        var systemCategoryId = Guid.Parse("656b9ad3-0e4a-4199-aa35-f38febf68dba");
        var systemCategoryDb = await _systemCategoryRepository.GetSystemCategoryById(systemCategoryId);
        
        await _systemCategoryRepository.DeleteSystemCategory(systemCategoryDb);
        
        var systemCategories = await context.SystemCategories.ToArrayAsync();
        
        Assert.Empty(systemCategories);
    }
}