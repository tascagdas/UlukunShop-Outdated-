using UlukunShopAPI.Application.DTOs.User;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse_DTO> CreateAsync(CreateUser_DTO model);
    Task UpdateRefreshToken(string refreshToken,AppUser? user,DateTime accessTokenDate,int addTimeToAccessToken);
}