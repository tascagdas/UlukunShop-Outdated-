using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.ShoppingCart.AddItemToCart;

public class AddItemToCartCommandRequest:IRequest<AddItemToCartCommandResponse>
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}