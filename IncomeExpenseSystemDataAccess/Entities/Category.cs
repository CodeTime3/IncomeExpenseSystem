namespace IncomeExpenseSystemDataAccess.Models;

public class Category
{
    public  Guid CategoryId { get; set; }
    public  string CategoryName { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}