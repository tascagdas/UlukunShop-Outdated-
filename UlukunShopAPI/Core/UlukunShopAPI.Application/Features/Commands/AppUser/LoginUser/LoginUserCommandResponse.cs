using UlukunShopAPI.Application.DTOs;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandResponse
{
}
//SOLID prensiplerinden S yi uygulamis olduk burada. 
public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
{
    public Token Token { get; set; }
}
public class LoginUserErrorCommandResponse: LoginUserCommandResponse
{
    public string Message { get; set; }
}