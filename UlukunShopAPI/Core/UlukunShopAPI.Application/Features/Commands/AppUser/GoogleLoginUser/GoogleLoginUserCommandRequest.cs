using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.GoogleLoginUser;

public class GoogleLoginUserCommandRequest:IRequest<GoogleLoginUserCommandResponse>
{
    public string Id { get; set; }
    public string IdToken { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhotoUrl { get; set; }
    public string Provider { get; set; }
}