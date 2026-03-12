namespace IncomeExpenseSystemDataAccess.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Category> Categories { get; set; }
    public DateTime? UserMailVerifiedAt { get; set; }
    public DateTime UserCreatedAt { get; set; }
    
    public User() {}
    
    public User(string userMail, string userPassword, DateTime userCreatedAt)
    {
        UserMail = userMail;
        UserPassword = userPassword;
        UserCreatedAt = userCreatedAt;
    }

    public User(Guid userId, string userMail, string userPassword, DateTime? userMailVerifiedAt, DateTime userCreatedAt)
    {
        UserId = userId;
        UserMail = userMail;
        UserPassword = userPassword;
        UserMailVerifiedAt = userMailVerifiedAt;
        UserCreatedAt = userCreatedAt;
    }
}