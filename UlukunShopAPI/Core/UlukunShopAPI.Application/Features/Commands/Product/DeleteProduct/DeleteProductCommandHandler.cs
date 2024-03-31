using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.Product.DeleteProduct;

public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommandRequest,DeleteProductCommandResponse>
{
    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}