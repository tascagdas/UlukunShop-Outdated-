using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.ShoppingCart.RemoveItemFromCart;

public class RemoveItemFromCartCommandRequest:IRequest<RemoveItemFromCartCommandResponse>
{
    public string ShoppingCartItemId { get; set; }
}