using UlukunShopAPI.Application.Repositories.Endpoint;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.Endpoint;

public class EndpointReadRepository : ReadRepository<Domain.Entities.Endpoint>, IEndpointReadRepository
{
    public EndpointReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}