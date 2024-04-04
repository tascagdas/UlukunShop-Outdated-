using UlukunShopAPI.Application.DTOs;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
    public Token Token { get; set; }
}