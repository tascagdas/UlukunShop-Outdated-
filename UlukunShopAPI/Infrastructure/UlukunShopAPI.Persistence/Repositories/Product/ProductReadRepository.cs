using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories;

public class ProductReadRepository:ReadRepository<Product>,IProductReadRespository
{
    public ProductReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}