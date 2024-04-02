using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UlukunShopAPI.Application.Abstractions.Token;

namespace UlukunShopAPI.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Application.DTOs.Token CreateAccessToken(int minute)
    {
        Application.DTOs.Token token = new();
        //Securitykeyin simetrigini aliyoruz.
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        //sifrelenmis kimlik olusturuyoruz.
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        //olusturacak token ayarlari yapiliyor
        token.Expiration = DateTime.UtcNow.AddMinutes(minute);

        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow, // NotBefore bu token uretildikten ne kadar sure sonra devreye girsin. eklemek icin .addminute denebiliyor.
            signingCredentials: signingCredentials
        );
        //Token olusturucu sinifindan bir ornek alalim.
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken=tokenHandler.WriteToken(securityToken);
        return token;
    }
}