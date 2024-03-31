using MediatR;

namespace UlukunShopAPI.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryHandler:IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}