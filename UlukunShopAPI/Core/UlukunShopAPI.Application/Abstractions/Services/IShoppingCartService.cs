using UlukunShopAPI.Application.ViewModels.ShoppingCarts;
using UlukunShopAPI.Domain.Entities;

namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IShoppingCartService
{
    public Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync();
    public Task AddItemToShoppingCartAsync(ShoppingCartItemCreateViewModel item);
    public Task UpdateShoppingCartItemQuantityAsync(ShoppingCartItemUpdateViewModel item);
    public Task DeleteShoppingCartItem(string shoppingCartItemId);
    
    public ShoppingCart? GetUserActiveBasket { get; }

}