using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.User;
using UlukunShopAPI.Application.Features.Commands.AppUser.CreateUser;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Persistence.Services;

public class UserService:IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponse_DTO> CreateAsync(CreateUser_DTO request)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id=Guid.NewGuid().ToString(),
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        }, request.Password);
        CreateUserResponse_DTO response = new() { Succeeded = result.Succeeded };
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
    }
}