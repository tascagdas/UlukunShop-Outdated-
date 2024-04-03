using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.FacebookLoginUser;

public class FacebookLoginUserCommandRequest:IRequest<FacebookLoginUserCommandResponse>
{
    public string AuthToken { get; set; }
}