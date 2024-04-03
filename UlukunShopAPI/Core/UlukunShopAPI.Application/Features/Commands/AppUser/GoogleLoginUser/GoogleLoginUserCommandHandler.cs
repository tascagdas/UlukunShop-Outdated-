using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Application.DTOs;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.GoogleLoginUser;

public class
    GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommandRequest, GoogleLoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;

    public GoogleLoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager,
        ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { "318442006015-9dpkendfiub1lr792vt03ri6mmvqi6ip.apps.googleusercontent.com" }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
        var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
        Domain.Entities.Identity.AppUser user =
            await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new Domain.Entities.Identity.AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.Email.Remove(payload.Email.IndexOf("@")),
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
        {
            await _userManager.AddLoginAsync(user, info);
        }
        else
        {
            throw new Exception("geçersiz dış kaynak logini");
        }

        Token token = _tokenHandler.CreateAccessToken(5);
        return new()
        {
            Token = token
        };
    }
}