using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;

namespace UlukunShopAPI.Application.Features.Queries.ShoppingCart.GetCartItems;

public class GetCartItemsQueryHandler:IRequestHandler<GetCartItemsQueryRequest,List<GetCartItemsQueryResponse>>
{
    private readonly IShoppingCartService _shoppingCartService;

    public GetCartItemsQueryHandler(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }


    public async Task<List<GetCartItemsQueryResponse>> Handle(GetCartItemsQueryRequest request, CancellationToken cancellationToken)
    {
        var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
        return cartItems.Select(item => new GetCartItemsQueryResponse
        {
            ShoppingCartItemId = item.Id.ToString(),
            Name = item.Product.Name,
            Price = item.Product.Price,
            Quantity = item.Quantity
        }).ToList();
    }
}