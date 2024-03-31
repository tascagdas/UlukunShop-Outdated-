using MediatR;

namespace UlukunShopAPI.Application.Features.Queries.ProductImageFile.GetProductImages;

public class GetProductImagesQueryHandler:IRequestHandler<GetProductImagesQueryRequest,GetProductImagesQueryResponse>
{
    public async Task<GetProductImagesQueryResponse> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}