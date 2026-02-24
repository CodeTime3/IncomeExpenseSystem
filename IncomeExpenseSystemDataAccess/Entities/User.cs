namespace IncomeExpenseSystemDataAccess.Models;

public class User
{
    public Guid UserId { get; set; }
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Category> Categories { get; set; }
}