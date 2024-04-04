using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Application.DTOs;
using UlukunShopAPI.Application.Exceptions;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }


    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 604800);
        return new LoginUserSuccessCommandResponse()
        {
            Token = token
        };
    }
}