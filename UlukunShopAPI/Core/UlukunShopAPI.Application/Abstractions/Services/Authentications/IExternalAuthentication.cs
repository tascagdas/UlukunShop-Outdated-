using UlukunShopAPI.Application.DTOs.GoogleLogin;

namespace UlukunShopAPI.Application.Abstractions.Services.Authentications;

public interface IExternalAuthentication
{
    Task<DTOs.Token> FacebookLoginAsync(string authToken,int accessTokenLifeTime);
    Task<DTOs.Token> GoogleLoginAsync(GoogleLoginRequest_DTO model,int accessTokenLifeTime);
}