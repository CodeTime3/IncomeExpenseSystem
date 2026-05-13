using IncomeExpenseSystemDataAccess.DBContext;
using IncomeExpenseSystemDataAccess.Repositories;
using IncomeExpenseSystemDomain.Entities;
using IncomeExpenseSystemDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class CategoryRepositoryTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private Category category;
    private ICategoryRepository _categoryRepository;
    
    [Fact]
    public async Task CreateCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        category = new Category
        (
            Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba"), 
            "Test",
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")
        );
        _categoryRepository = new CategoryRepository(context);
        
        var categoryDb = await _categoryRepository.CreateCategory(category);
        
        Assert.Equal("Test", categoryDb.CategoryName);
    }
    
    [Fact]
    public async Task GetCategoryById_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _categoryRepository = new CategoryRepository(context);

        var categoryId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba");
        var categoryDb = await _categoryRepository.GetCategoryById(categoryId);
        
        Assert.NotNull(categoryDb);
    }
    
    [Fact]
    public async Task GetCategoriesByUserId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _categoryRepository = new CategoryRepository(context);

        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var categories = await _categoryRepository.GetCategoriesByUserId(userId);
        
        Assert.NotNull(categories);
        Assert.Single(categories);
    }

    [Fact]
    public async Task UpdateCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _categoryRepository = new CategoryRepository(context);
        category = new Category
        (
            Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba"), 
            "test",
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")
        );
        
        var categoryDb = await _categoryRepository.UpdateCategory(category);
        
        Assert.Equal("test", categoryDb.CategoryName);
    }

    [Fact]
    public async Task DeleteSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        _categoryRepository = new CategoryRepository(context);
        
        var categoryId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba");
        var categoryDb = await _categoryRepository.GetCategoryById(categoryId);
        
        await _categoryRepository.DeleteCategory(categoryDb);
        
        var categories = await context.Categories.ToArrayAsync();
        
        Assert.Empty(categories);
    }
}