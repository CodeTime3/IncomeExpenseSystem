namespace IncomeExpenseSystemDataAccess.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Category> Categories { get; set; }

    public User(string userMail, string userPassword)
    {
        UserMail = userMail;
        UserPassword = userPassword;
    }
}