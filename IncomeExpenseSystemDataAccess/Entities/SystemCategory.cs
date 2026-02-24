namespace IncomeExpenseSystemDataAccess.Models;

public class SystemCategory
{
    public Guid SystemCategoryId { get; set; }
    public string SystemCategoryName { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}