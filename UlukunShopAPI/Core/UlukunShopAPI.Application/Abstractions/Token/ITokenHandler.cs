using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(int second, AppUser appUser);
    string CreateRefreshToken();
}