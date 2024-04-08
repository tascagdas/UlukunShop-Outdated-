using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.ShoppingCart;
using UlukunShopAPI.Application.Repositories.ShoppingCartItem;
using UlukunShopAPI.Application.ViewModels.ShoppingCarts;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Persistence.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IShoppingCartWriteRepository _cartWriteRepository;
    private readonly IShoppingCartItemWriteRepository _cartItemWrite;
    private readonly IShoppingCartItemReadRepository _cartItemRead;
    private readonly IShoppingCartReadRepository _cartRead;

    public ShoppingCartService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager,
        IOrderReadRepository orderReadRepository, IShoppingCartWriteRepository cartWriteRepository,
        IShoppingCartItemWriteRepository cartItemWrite, IShoppingCartItemReadRepository cartItemRead,
        IShoppingCartReadRepository cartRead)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _cartWriteRepository = cartWriteRepository;
        _cartItemWrite = cartItemWrite;
        _cartItemRead = cartItemRead;
        _cartRead = cartRead;
    }

    private async Task<ShoppingCart?> ContextUser()
    {
        var username = _contextAccessor.HttpContext?.User.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            AppUser? user = await _userManager.Users.Include(u => u.ShoppingCarts)
                .FirstOrDefaultAsync(u => u.UserName == username);

            var _shoppingCart = from shoppingCart in user.ShoppingCarts
                join order in _orderReadRepository.Table on shoppingCart.Id equals order.Id into cartOrders
                from order in cartOrders.DefaultIfEmpty()
                select new
                {
                    ShoppingCart = shoppingCart,
                    Order = order
                };
            ShoppingCart? targetShoppingCart = null;
            if (_shoppingCart.Any(cart => cart.Order is null))
            {
                targetShoppingCart = _shoppingCart.FirstOrDefault(cart => cart.Order is null)?.ShoppingCart;
            }
            else
            {
                targetShoppingCart = new ();
                user.ShoppingCarts.Add(targetShoppingCart);
            }

            await _cartWriteRepository.SaveAsync();
            return targetShoppingCart;
        }

        throw new Exception("Beklenmeyen bir hata (user'a ulaşılamadı??)");
    }

    public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync()
    {
        ShoppingCart? shoppingCart = await ContextUser();
        ShoppingCart result = await _cartRead.Table.Include(cart => cart.ShoppingCartItems)
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(cart => cart.Id == shoppingCart.Id);
        return result.ShoppingCartItems.ToList();
    }

    public async Task AddItemToShoppingCartAsync(ShoppingCartItemCreateViewModel item)
    {
        ShoppingCart? shoppingCart = await ContextUser();
        if (shoppingCart != null)
        {
            ShoppingCartItem shoppingCartItem = await _cartItemRead.GetSingleAsync(cartItem =>
                cartItem.ShoppingCartId == shoppingCart.Id && cartItem.ProductId == Guid.Parse(item.ProductId));

            if (shoppingCartItem != null)
            {
                shoppingCartItem.Quantity++;
            }
            else
            {
                await _cartItemWrite.AddAsync(new()
                {
                    ShoppingCartId = shoppingCart.Id,
                    ProductId = Guid.Parse(item.ProductId),
                    Quantity = item.Quantity
                });
            }
            await _cartItemWrite.SaveAsync();
        }
    }

    public async Task UpdateShoppingCartItemQuantityAsync(ShoppingCartItemUpdateViewModel item)
    {
        ShoppingCartItem? shoppingCartItem = await _cartItemRead.GetByIdAsync(item.ShoppingCartItemId);
        if (shoppingCartItem!=null)
        {
            shoppingCartItem.Quantity = item.Quantity;
            await _cartItemWrite.SaveAsync();
        }
    }

    public async Task DeleteShoppingCartItem(string shoppingCartItemId)
    {
        ShoppingCartItem? shoppingCartItem = await _cartItemRead.GetByIdAsync(shoppingCartItemId);
        if (shoppingCartItem!=null)
        {
            _cartItemWrite.Remove(shoppingCartItem);
            await _cartItemWrite.SaveAsync();
        }
    }
    
    public ShoppingCart? GetUserActiveBasket
    {
        get
        {
            ShoppingCart? basket = ContextUser().Result;
            return basket;
        }
    }
}