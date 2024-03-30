using MediatR;
using UlukunShopAPI.Application.RequestParameters;

namespace UlukunShopAPI.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQueryRequest:IRequest<GetAllProductsQueryResponse>
{
    // public Pagination Pagination { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5; 
}