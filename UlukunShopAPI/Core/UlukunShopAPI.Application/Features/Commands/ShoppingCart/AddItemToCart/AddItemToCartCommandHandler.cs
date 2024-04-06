using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.ViewModels.ShoppingCarts;

namespace UlukunShopAPI.Application.Features.Commands.ShoppingCart.AddItemToCart;

public class AddItemToCartCommandHandler:IRequestHandler<AddItemToCartCommandRequest,AddItemToCartCommandResponse>
{
    private readonly IShoppingCartService _shoppingCartService;

    public AddItemToCartCommandHandler(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public async Task<AddItemToCartCommandResponse> Handle(AddItemToCartCommandRequest request, CancellationToken cancellationToken)
    {
       await _shoppingCartService.AddItemToShoppingCartAsync(new ShoppingCartItemCreateViewModel()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        });
        return new AddItemToCartCommandResponse();
    }
}