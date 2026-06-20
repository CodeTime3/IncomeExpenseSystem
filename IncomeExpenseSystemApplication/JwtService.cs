using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IncomeExpenseSystemDomain.Models;

namespace IncomeExpenseSystemApplication;

public class JwtService : IJwtService
{
    private JwtModel _model;

    public JwtService(JwtModel model)
    {
        _model = model;
    }
    
    public string CreateJwt(Guid userId)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, userId.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_model.Token));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken
        (
            issuer: _model.Issuer,
            audience: _model.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    } 
}
