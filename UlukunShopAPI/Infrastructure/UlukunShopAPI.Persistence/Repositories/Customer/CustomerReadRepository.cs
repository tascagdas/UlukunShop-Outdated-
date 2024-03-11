using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories;

public class CustomerReadRepository:ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}