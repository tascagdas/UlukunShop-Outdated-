using UlukunShopAPI.Application.Repositories.ShoppingCartItem;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.ShoppingCartItem;

public class ShoppingCartItemWriteRepository:WriteRepository<Domain.Entities.ShoppingCartItem>,IShoppingCartItemWriteRepository
{
    public ShoppingCartItemWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}