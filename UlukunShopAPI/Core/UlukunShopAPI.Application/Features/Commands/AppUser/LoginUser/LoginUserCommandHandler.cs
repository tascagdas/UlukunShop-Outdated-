using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Application.DTOs;
using UlukunShopAPI.Application.Exceptions;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler:IRequestHandler<LoginUserCommandRequest,LoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
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
            throw new UserNotFoundException();
        }
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
            //Buraya kadar gelebildiginde authentication basarili burada artik yetkiler belirlenicek

            Token token = _tokenHandler.CreateAccessToken(5);
            
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }
        // return new LoginUserErrorCommandResponse()
        // {
        //     Message = "kullanici bilgileri hatali"
        // };

        throw new AuthenticationErrorException();
    }
}