using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Exceptions;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id=Guid.NewGuid().ToString(),
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        }, request.Password);
        CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };
        if (result.Succeeded)
        {
            response.Message = "Kullanici basariyla olusturuldu.";
        }
        else
        {
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code} / {error.Description}\n";
            }
        }

        return response;

        // throw new UserCreateFailedException();
    }
}