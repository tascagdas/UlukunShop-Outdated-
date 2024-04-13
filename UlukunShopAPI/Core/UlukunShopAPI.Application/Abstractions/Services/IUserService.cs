using UlukunShopAPI.Application.DTOs.User;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse_DTO> CreateAsync(CreateUser_DTO model);
    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
    Task<List<ListUser>> GetAllUsersAsync(int page, int size);
    int TotalUsersCount { get; }
    Task AssignRoleToUserAsnyc(string userId, string[] roles);
    Task<string[]> GetRolesToUserAsync(string userIdOrName);
    Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
}