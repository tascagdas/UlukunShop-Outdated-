using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Repositories.ShoppingCartItem;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.ShoppingCartItem;

public class ShoppingCartItemReadRepository:ReadRepository<Domain.Entities.ShoppingCartItem>,IShoppingCartItemReadRepository
{
    public ShoppingCartItemReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}