using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories;

public class CustomerWriteRespository:WriteRepository<Customer>,ICustomerWriteRepository
{
    public CustomerWriteRespository(UlukunAPIDbContext context) : base(context)
    {
        
    }
}