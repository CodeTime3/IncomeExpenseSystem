namespace IncomeExpenseSystemDataAccess.Models;

public class Expense
{
    public Guid IdExpense { get; set; }
    public decimal AmountExpense { get; set; } = 0;
    public string DescriptionExpense { get; set; } =  string.Empty;
    public DateTime DateExpense { get; set; } = DateTime.Today;
}