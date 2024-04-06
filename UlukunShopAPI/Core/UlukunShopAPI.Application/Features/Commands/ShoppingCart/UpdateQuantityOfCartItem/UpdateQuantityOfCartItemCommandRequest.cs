using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.ShoppingCart.UpdateQuantityOfCartItem;

public class UpdateQuantityOfCartItemCommandRequest:IRequest<UpdateQuantityOfCartItemCommandResponse>
{
    public string ShoppingCartItemId { get; set; }
    public int Quantity { get; set; }
}