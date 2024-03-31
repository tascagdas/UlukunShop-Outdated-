using MediatR;
using UlukunShopAPI.Application.Repositories;

namespace UlukunShopAPI.Application.Features.Commands.Product.DeleteProduct;

public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommandRequest,DeleteProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.RemoveAsync(request.Id);
        await _productWriteRepository.SaveAsync();
        return new();
    }
}