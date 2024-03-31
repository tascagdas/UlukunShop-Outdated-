using UlukunShopAPI.Domain.Entities;

namespace UlukunShopAPI.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryResponse
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}