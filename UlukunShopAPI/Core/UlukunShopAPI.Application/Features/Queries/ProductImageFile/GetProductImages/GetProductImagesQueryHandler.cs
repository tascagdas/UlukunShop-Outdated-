using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UlukunShopAPI.Application.Repositories;

namespace UlukunShopAPI.Application.Features.Queries.ProductImageFile.GetProductImages;

public class GetProductImagesQueryHandler:IRequestHandler<GetProductImagesQueryRequest,List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRespository _productReadRepository;
    private readonly IConfiguration _configuration;

    public GetProductImagesQueryHandler(IProductReadRespository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product? product= await _productReadRepository.Table.Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
        return product?.ProductImages.Select(p => new GetProductImagesQueryResponse
        {
            Path=$"{_configuration["BaseStorageUrl"]}/{p.Path}",
            FileName=p.FileName,
            Id=p.Id
        }).ToList();
    }
}