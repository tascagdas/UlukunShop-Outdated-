using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.ViewModels.ShoppingCarts;

namespace UlukunShopAPI.Application.Features.Commands.ShoppingCart.UpdateQuantityOfCartItem;

public class UpdateQuantityOfCartItemCommandHandler:IRequestHandler<UpdateQuantityOfCartItemCommandRequest,UpdateQuantityOfCartItemCommandResponse>
{
    private readonly IShoppingCartService _shoppingCartService;

    public UpdateQuantityOfCartItemCommandHandler(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public async Task<UpdateQuantityOfCartItemCommandResponse> Handle(UpdateQuantityOfCartItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _shoppingCartService.UpdateShoppingCartItemQuantityAsync(new ShoppingCartItemUpdateViewModel()
        {
            Quantity = request.Quantity,
            ShoppingCartItemId = request.ShoppingCartItemId
        });
        return new UpdateQuantityOfCartItemCommandResponse();
    }
}