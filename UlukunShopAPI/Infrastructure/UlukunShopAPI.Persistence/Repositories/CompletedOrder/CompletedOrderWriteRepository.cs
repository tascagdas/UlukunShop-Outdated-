using UlukunShopAPI.Application.Repositories.CompletedOrder;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.CompletedOrder;

public class CompletedOrderWriteRepository:WriteRepository<Domain.Entities.CompletedOrder>,ICompletedOrderWriteRepository
{
    public CompletedOrderWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}