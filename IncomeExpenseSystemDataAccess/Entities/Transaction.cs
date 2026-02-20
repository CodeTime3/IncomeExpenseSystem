namespace IncomeExpenseSystemDataAccess.Models;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public decimal TransactionAmount { get; set; } = 0;
    public string TransactionDescription { get; set; } =  string.Empty;
    public DateTime TransactionDate { get; set; } = DateTime.Today;
    public TransactionType TransactionType { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}