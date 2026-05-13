using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using IncomeExpenseSystemDomain.Models;

namespace IncomeExpenseSystemApplication;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateJwt(UserModel user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.UserId.ToString())
        };

        var token = _configuration.GetValue<string>("JwtSettings:Token");
        if (string.IsNullOrEmpty(token))
        {
            return "";
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken
        (
            issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
            audience: _configuration.GetValue<string>("JwtSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    } 
}
