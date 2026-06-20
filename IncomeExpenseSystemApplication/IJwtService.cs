namespace IncomeExpenseSystemApplication;

public interface IJwtService
{
    string CreateJwt(Guid userId);
}