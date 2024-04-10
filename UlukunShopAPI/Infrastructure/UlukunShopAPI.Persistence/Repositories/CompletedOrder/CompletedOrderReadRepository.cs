using UlukunShopAPI.Application.Repositories.CompletedOrder;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.CompletedOrder;

public class CompletedOrderReadRepository:ReadRepository<Domain.Entities.CompletedOrder>,ICompletedOrderReadRepository
{
    public CompletedOrderReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}