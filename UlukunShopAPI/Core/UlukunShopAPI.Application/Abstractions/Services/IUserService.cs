using UlukunShopAPI.Application.DTOs.User;

namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse_DTO> CreateAsync(CreateUser_DTO model);
}