namespace IncomeExpenseSystemDataAccess.Entities;

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
    public Guid SystemCategoryId { get; set; }
    public SystemCategory SystemCategory { get; set; }
    
    public Transaction() {}

    public Transaction(decimal transactionAmount, string transactionDescription, DateTime transactionDate, TransactionType transactionType, Guid userId, Guid categoryId, Guid systemCategoryId)
    {
        TransactionAmount = transactionAmount;
        TransactionDescription = transactionDescription;
        TransactionDate = transactionDate;
        TransactionType = transactionType;
        UserId = userId;
        CategoryId = categoryId;
        SystemCategoryId = systemCategoryId;
    }
    
    public Transaction(Guid transactionId, decimal transactionAmount, string transactionDescription, DateTime transactionDate, TransactionType transactionType, Guid userId, Guid categoryId, Guid systemCategoryId)
    {
        TransactionId = transactionId;
        TransactionAmount = transactionAmount;
        TransactionDescription = transactionDescription;
        TransactionDate = transactionDate;
        TransactionType = transactionType;
        UserId = userId;
        CategoryId = categoryId;
        SystemCategoryId = systemCategoryId;
    }
}