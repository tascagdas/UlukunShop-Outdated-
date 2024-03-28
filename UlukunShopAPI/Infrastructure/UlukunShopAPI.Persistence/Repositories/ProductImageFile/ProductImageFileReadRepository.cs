using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.ProductImageFile;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.ProductImageFile;

public class ProductImageFileReadRepository:ReadRepository<Domain.Entities.ProductImageFile>,IProductImageFileReadRepository
{
    public ProductImageFileReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}