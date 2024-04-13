using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.User;
using UlukunShopAPI.Application.Exceptions;
using UlukunShopAPI.Application.Features.Commands.AppUser.CreateUser;
using UlukunShopAPI.Application.Helpers;
using UlukunShopAPI.Application.Repositories.Endpoint;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Persistence.Services;

public class UserService:IUserService
{
    private readonly UserManager<AppUser?> _userManager;
    private readonly IEndpointReadRepository _endpointReadRepository;

    public UserService(UserManager<AppUser?> userManager, IEndpointReadRepository endpointReadRepository)
    {
        _userManager = userManager;
        _endpointReadRepository = endpointReadRepository;
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

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser? user,DateTime accessTokenDate,int addTimeToAccessToken)
    {
        if (user!=null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addTimeToAccessToken);
            await _userManager.UpdateAsync(user);
        }
        else
        {
            throw new UserNotFoundException();
        }
    }
    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = resetToken.UrlDecode();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException();
        }
    }
    public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
    {
        var users = await _userManager.Users
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        return users.Select(user => new ListUser
        {
            Id = user.Id,
            Email = user.Email,
            NameSurname = user.FirstName,
            TwoFactorEnabled = user.TwoFactorEnabled,
            UserName = user.UserName
        }).ToList();
    }

    public int TotalUsersCount => _userManager.Users.Count();

    public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRolesAsync(user, roles);
        }
    }
    public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
    {
        AppUser user = await _userManager.FindByIdAsync(userIdOrName);
        if (user == null)
            user = await _userManager.FindByNameAsync(userIdOrName);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
        return new string[] { };
    }
    public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
    {
        var userRoles = await GetRolesToUserAsync(name);

        if (!userRoles.Any())
            return false;

        Endpoint? endpoint = await _endpointReadRepository.Table
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Code == code);

        if (endpoint == null)
            return false;

        var hasRole = false;
        var endpointRoles = endpoint.Roles.Select(r => r.Name);
        

        foreach (var userRole in userRoles)
        {
            foreach (var endpointRole in endpointRoles)
                if (userRole == endpointRole)
                    return true;
        }

        return false;
    }
}