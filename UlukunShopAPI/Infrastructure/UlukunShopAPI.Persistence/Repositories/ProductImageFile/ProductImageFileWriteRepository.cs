using UlukunShopAPI.Application.Repositories.ProductImageFile;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.ProductImageFile;

public class ProductImageFileWriteRepository:WriteRepository<Domain.Entities.ProductImageFile>,IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}