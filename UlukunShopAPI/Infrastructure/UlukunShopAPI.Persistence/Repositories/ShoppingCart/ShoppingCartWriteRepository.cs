using UlukunShopAPI.Application.Repositories.ShoppingCart;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.ShoppingCart;

public class ShoppingCartWriteRepository:WriteRepository<Domain.Entities.ShoppingCart>,IShoppingCartWriteRepository 
{
    public ShoppingCartWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}