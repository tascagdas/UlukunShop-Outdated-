using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Exceptions;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler:IRequestHandler<LoginUserCommandRequest,LoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

    public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Identity.AppUser user=await _userManager.FindByNameAsync(request.UsernameOrEmail);
        if (user==null)
        {
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        }

        if (user==null)
        {
            throw new UserNotFoundException("Oturum acma bilgileriniz hatali");
        }

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
            //Buraya kadar gelebildiginde authentication basarili burada artik yetkiler belirlenicek
        }
        return new();
    }
}