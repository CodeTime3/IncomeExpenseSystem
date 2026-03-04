namespace IncomeExpenseSystemDataAccess.Entities;

public class Category
{
    public  Guid CategoryId { get; set; }
    public  string CategoryName { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Category() {}
    
    public Category(string categoryName, Guid userId)
    {
        CategoryName = categoryName;
        UserId = userId;
    }
    
    public Category(Guid categoryId, string categoryName, Guid userId)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
        UserId = userId;
    }
}