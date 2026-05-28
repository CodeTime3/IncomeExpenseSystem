namespace IncomeExpenseSystemDomain.Models;

public class EmailModel
{
    public string UserEmail { get; set; }
    public string EmailHost { get; set; }
    public int EmailPort { get; set; }
    public string EmailUsername { get; set; }
    public string EmailPassword { get; set; }
}