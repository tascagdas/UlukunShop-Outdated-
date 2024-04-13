using UlukunShopAPI.Application.Repositories.Endpoint;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.Endpoint;

public class EndpointWriteRepository : WriteRepository<Domain.Entities.Endpoint>, IEndpointWriteRepository
{
    public EndpointWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}