using MediatR;

namespace UlukunShopAPI.Application.Features.Queries.AppUser.GetAllUsersQuery;

public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
{
    public int Page { get; set; }
    public int Size { get; set; }
}