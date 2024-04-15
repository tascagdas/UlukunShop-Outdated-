using System.Text.Json;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Application.DTOs;
using UlukunShopAPI.Application.DTOs.FacebookAccessToken;
using UlukunShopAPI.Application.DTOs.GoogleLogin;
using UlukunShopAPI.Application.Exceptions;
using UlukunShopAPI.Application.Features.Commands.AppUser.LoginUser;
using UlukunShopAPI.Application.Helpers;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Persistence.Services;

//Bu servis refaktoring esnasinda buraya alindi ve handlerler icinde yapilan islemler artik burada yapiliyor. bazi yorum satirlari ve kodun nasil calistigini daha kolay anlamak icin repositoryden onceki commitlere bakilabilir. bu commit tarihi 3 nisan 2024
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser?> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly SignInManager<AppUser?> _signInManager;
    private readonly IUserService _userService;
    readonly IMailService _mailService;

    public AuthService(IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        UserManager<AppUser?> userManager,
        ITokenHandler tokenHandler, SignInManager<AppUser?> signInManager, IUserService userService, IMailService mailService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
        _mailService = mailService;
        _httpClient = httpClientFactory.CreateClient();
    }

    async Task<Token> CreateExternalUserAsync(AppUser? user, string firstName, string lastName, string email,
        UserLoginInfo info, int accessTokenLifeTime)
    {
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
        {
            await _userManager.AddLoginAsync(user, info);
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
            return token;
        }

        throw new Exception("geçersiz dış kaynak logini");
    }

    public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        string accessTokenResponse = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        FacebookAccessTokenResponse_DTO? facebookAccessTokenResponse =
            JsonSerializer.Deserialize<FacebookAccessTokenResponse_DTO>(accessTokenResponse);

        string userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        FacebookAccessTokenValidation_DTO? validation =
            JsonSerializer.Deserialize<FacebookAccessTokenValidation_DTO>(userAccessTokenValidation);

        if (validation?.Data.IsValid != null)
        {
            string userInfoResponse =
                await _httpClient.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateExternalUserAsync(user, userInfo.FullName, userInfo.FullName, userInfo.Email, info,
                accessTokenLifeTime);
        }

        throw new Exception("Invalid external authentication.");
    }

    public async Task<Token> GoogleLoginAsync(GoogleLoginRequest_DTO model, int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
        var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
        AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        return await CreateExternalUserAsync(user, model.FirstName, model.LastName, model.Email, info,
            accessTokenLifeTime);
    }


    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        AppUser? user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(usernameOrEmail);
        }

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.Succeeded)
        {
            //Buraya kadar gelebildiginde authentication basarili burada artik yetkiler belirlenicek

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 60480);
            return token;
        }
        throw new AuthenticationErrorException();
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user=await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user!=null && user?.RefreshTokenEndDate>DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(604800,user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 60480);
            return token;
        }
        else
        {
            throw new UserNotFoundException();
        }
    }
    
    public async Task PasswordResetAsnyc(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
            //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
            resetToken = resetToken.UrlEncode();

            await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
        }
    }

    public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
            //resetToken = Encoding.UTF8.GetString(tokenBytes);
            resetToken = resetToken.UrlDecode();

            return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        }
        return false;
    }
}