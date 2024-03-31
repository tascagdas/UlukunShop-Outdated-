using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;

public class DeleteProductImageCommandHandler:IRequestHandler<DeleteProductImageCommandRequest,DeleteProductImageCommandResponse>
{
    public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}