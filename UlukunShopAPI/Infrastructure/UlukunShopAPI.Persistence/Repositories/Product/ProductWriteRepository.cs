using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories;

public class ProductWriteRepository:WriteRepository<Product>,IProductWriteRepository
{
    public ProductWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}