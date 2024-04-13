using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;

namespace UlukunShopAPI.Application.Features.Queries.AppUser.GetRolesToUser;

public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
{
    readonly IUserService _userService;

    public GetRolesToUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
    {
        var userRoles = await _userService.GetRolesToUserAsync(request.UserId);
        return new()
        {
            UserRoles = userRoles
        };
    }
}