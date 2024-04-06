namespace UlukunShopAPI.Application.Features.Queries.ShoppingCart.GetCartItems;

public class GetCartItemsQueryResponse
{
    public string CartItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}