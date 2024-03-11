using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories;

public class OrderReadRepository:ReadRepository<Order>,IOrderReadRepository
{
    public OrderReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}