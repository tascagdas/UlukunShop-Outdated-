using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler:IRequestHandler<RefreshTokenLoginCommandRequest,RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
        return new()
        {
            Token = token
        };
    }
}