using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Application.DTOs;
using UlukunShopAPI.Application.DTOs.GoogleLogin;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.GoogleLoginUser;

public class GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommandRequest, GoogleLoginUserCommandResponse>
{
    private readonly IAuthService _authService;

    public GoogleLoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request, CancellationToken cancellationToken)
    {

        Token token = await _authService.GoogleLoginAsync(new GoogleLoginRequest_DTO()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Name = request.Name,
            Id = request.Id,
            IdToken = request.IdToken,
            PhotoUrl = request.PhotoUrl,
            Provider = request.Provider
        },604800);
        return new()
        {
            Token = token
        };
    }


}