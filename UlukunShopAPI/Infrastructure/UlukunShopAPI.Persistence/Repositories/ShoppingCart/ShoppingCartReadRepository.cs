using UlukunShopAPI.Application.Repositories.ShoppingCart;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.ShoppingCart;

public class ShoppingCartReadRepository:ReadRepository<Domain.Entities.ShoppingCart>,IShoppingCartReadRepository 
{
    public ShoppingCartReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}