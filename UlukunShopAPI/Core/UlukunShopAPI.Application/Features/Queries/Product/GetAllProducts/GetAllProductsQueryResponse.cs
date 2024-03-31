namespace UlukunShopAPI.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductsQueryResponse
{
    public int TotalCount { get; set; }
    public object Products { get; set; }
}