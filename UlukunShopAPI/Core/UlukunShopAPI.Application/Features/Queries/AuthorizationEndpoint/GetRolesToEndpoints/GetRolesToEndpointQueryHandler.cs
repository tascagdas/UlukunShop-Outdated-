using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;

namespace UlukunShopAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoints;

public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
{
    readonly IAuthorizationEndpointService _authorizationEndpointService;

    public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code, request.Menu);
        return new()
        {
            Roles = datas
        };
    }
}