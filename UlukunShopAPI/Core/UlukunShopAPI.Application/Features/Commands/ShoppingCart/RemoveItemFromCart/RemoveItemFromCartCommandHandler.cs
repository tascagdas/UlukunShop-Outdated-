using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;

namespace UlukunShopAPI.Application.Features.Commands.ShoppingCart.RemoveItemFromCart;

public class RemoveItemFromCartCommandHandler:IRequestHandler<RemoveItemFromCartCommandRequest,RemoveItemFromCartCommandResponse>
{
    private readonly IShoppingCartService _shoppingCartService;

    public RemoveItemFromCartCommandHandler(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public async Task<RemoveItemFromCartCommandResponse> Handle(RemoveItemFromCartCommandRequest request, CancellationToken cancellationToken)
    {
         await _shoppingCartService.DeleteShoppingCartItem(request.ShoppingCartItemId);
         return new RemoveItemFromCartCommandResponse();
    }
}