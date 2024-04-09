namespace UlukunShopAPI.Application.Features.Queries.Order;

public class GetAllOrdersQueryResponse
{
    public int TotalOrderCount { get; set; }
    public object Orders { get; set; }
}