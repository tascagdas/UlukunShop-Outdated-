using MediatR;
using Microsoft.AspNetCore.Identity;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.User;
using UlukunShopAPI.Application.Exceptions;

namespace UlukunShopAPI.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        CreateUserResponse_DTO response = await _userService.CreateAsync(new CreateUser_DTO()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            RePassword = request.RePassword,
            Username = request.Username
        });
        return new CreateUserCommandResponse()
        {
            Message = response.Message,
            Succeeded = response.Succeeded
        };
    }
}