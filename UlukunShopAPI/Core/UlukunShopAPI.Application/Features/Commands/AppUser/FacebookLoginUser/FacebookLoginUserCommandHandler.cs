using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Application.DTOs;
using UlukunShopAPI.Application.DTOs.FacebookAccessToken;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.FacebookLoginUser;

public class
    FacebookLoginUserCommandHandler : IRequestHandler<FacebookLoginUserCommandRequest, FacebookLoginUserCommandResponse>
{
    private readonly IAuthService _authService;

    public FacebookLoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<FacebookLoginUserCommandResponse> Handle(FacebookLoginUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var token=await _authService.FacebookLoginAsync(request.AuthToken,604800);
        return new()
        {
            Token = token
        };
    }
}
