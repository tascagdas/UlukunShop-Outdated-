using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories;

public class OrderWriteRepository:WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}