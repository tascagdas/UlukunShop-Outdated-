using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;

namespace UlukunShopAPI.Application.Features.Commands.AuthorizationEndpoints;

public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
{
    readonly IAuthorizationEndpointService _authorizationEndpointService;

    public AssignRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
    {
        await _authorizationEndpointService.AssignRoleEndpointAsync(request.Roles, request.Menu, request.Code, request.Type);
        return new()
        {

        };
    }
}