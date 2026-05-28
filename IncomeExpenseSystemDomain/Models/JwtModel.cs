namespace IncomeExpenseSystemDomain.Models;

public class JwtModel
{
    public string Token { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}