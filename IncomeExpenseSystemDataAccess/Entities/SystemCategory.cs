namespace IncomeExpenseSystemDataAccess.Entities;

public class SystemCategory
{
    public Guid SystemCategoryId { get; set; }
    public string SystemCategoryName { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    
    public SystemCategory() {}

    public SystemCategory(string systemCategoryName)
    {
        SystemCategoryName = systemCategoryName;
    }
    
    public SystemCategory(Guid systemCategoryId, string systemCategoryName)
    {
        SystemCategoryId = systemCategoryId;
        SystemCategoryName = systemCategoryName;
    }
}