using MediatR;
using UlukunShopAPI.Application.Repositories;

namespace UlukunShopAPI.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductsQueryHandler:IRequestHandler<GetAllProductsQueryRequest,GetAllProductsQueryResponse>
{
    private readonly IProductReadRespository _productReadRepository;
    public GetAllProductsQueryHandler(IProductReadRespository productReadRespository)
    {
        _productReadRepository = productReadRespository;
    }
    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        //tracking devre disi daha verimli calisma icin.
        var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new
        {
            p.Id,
            p.Name,
            p.Stock,
            p.Price,
            p.CreatedDate,
            p.UpdatedDate
        }).ToList();

        return new()
        {
            Products = products,
            TotalCount = totalCount
        };
    }
}