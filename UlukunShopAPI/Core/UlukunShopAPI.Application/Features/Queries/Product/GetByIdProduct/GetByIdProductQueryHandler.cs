using MediatR;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.ProductImageFile;

namespace UlukunShopAPI.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryHandler:IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    private readonly IProductReadRespository _productReadRepository;

    public GetByIdProductQueryHandler(IProductReadRespository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
        return new()
        {
            Name = product.Name,
            Stock = product.Stock,
            Price = product.Price
        };
    }
}