using IncomeExpenseSystemDataAccess.DataAccess;
using IncomeExpenseSystemDataAccess.DataAccess.Servicies;
using IncomeExpenseSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpenseSystemTest;

public class CategoryServiceTest
{
    DbContextOptions<IncomeExpenseSystemDbContext> options = new DbContextOptionsBuilder<IncomeExpenseSystemDbContext>()
        .UseMySQL("server=localhost;uid=Daniele;pwd=CT22d03p06;database=IncomeExpenseSystem")
        .Options;

    private Category category;
    private CategoryService categoryService;
    
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
        categoryService = new CategoryService(context);
        
        var categoryDb = await categoryService.CreateCategory(category);
        
        Assert.Equal("Test", categoryDb.CategoryName);
    }
    
    [Fact]
    public async Task GetCategoryById_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        categoryService = new CategoryService(context);

        var categoryId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba");
        var categoryDb = await categoryService.GetCategoryById(categoryId);
        
        Assert.NotNull(categoryDb);
    }
    
    [Fact]
    public async Task GetCategoriesByUserId_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        categoryService = new CategoryService(context);

        var userId = Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38");
        var categories = await categoryService.GetCategoriesByUserId(userId);
        
        Assert.NotNull(categories);
        Assert.Single(categories);
    }

    [Fact]
    public async Task UpdateCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        categoryService = new CategoryService(context);
        category = new Category
        (
            Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba"), 
            "test",
            Guid.Parse("532b2bfa-87ae-435a-b91b-76297442ac38")
        );
        
        var categoryDb = await categoryService.UpdateCategory(category);
        
        Assert.Equal("test", categoryDb.CategoryName);
    }

    [Fact]
    public async Task DeleteSystemCategory_should_work()
    {
        await using var context = new IncomeExpenseSystemDbContext(options);
        categoryService = new CategoryService(context);
        
        var categoryId = Guid.Parse("939b9ad3-0e4a-4199-aa35-f38febf68dba");
        var categoryDb = await categoryService.GetCategoryById(categoryId);
        
        await categoryService.DeleteCategory(categoryDb);
        
        var categories = await context.Categories.ToArrayAsync();
        
        Assert.Empty(categories);
    }
}