using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Application.DTOs;
using UlukunShopAPI.Application.DTOs.FacebookAccessToken;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.FacebookLoginUser;

public class
    FacebookLoginUserCommandHandler : IRequestHandler<FacebookLoginUserCommandRequest, FacebookLoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly HttpClient _httpClient;

    public FacebookLoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager,
        ITokenHandler tokenHandler,
        IHttpClientFactory httpClientFactory)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<FacebookLoginUserCommandResponse> Handle(FacebookLoginUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        string accessTokenResponse = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id=1155344239096381&client_secret=6e517e55164abd3dcd1883d7d1d97f2c&grant_type=client_credentials");

        FacebookAccessTokenResponse_DTO facebookAccessTokenResponse =
            JsonSerializer.Deserialize<FacebookAccessTokenResponse_DTO>(accessTokenResponse);

        string userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponse.AccessToken}");

        FacebookAccessTokenValidation_DTO validation =
            JsonSerializer.Deserialize<FacebookAccessTokenValidation_DTO>(userAccessTokenValidation);

        if (validation.Data.IsValid)
        {
            string userInfoResponse =
                await _httpClient.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");

            FacebookUserInfoResponse userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            Domain.Entities.Identity.AppUser user =
                await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userInfo.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = userInfo.Email,
                        UserName = userInfo.Email,
                        FirstName = userInfo.FullName
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info);

                Token token = _tokenHandler.CreateAccessToken(5);
                return new()
                {
                    Token = token
                };
            }
        }

        throw new Exception("Invalid external authentication.");
    }
}
