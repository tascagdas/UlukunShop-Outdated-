using MediatR;
using UlukunShopAPI.Application.Repositories;

namespace UlukunShopAPI.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommandRequest,UpdateProductCommandResponse>
{
    private readonly IProductReadRespository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductCommandHandler(IProductReadRespository productReadRespository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRespository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Stock = request.Stock;
        product.Name = request.Name;
        product.Stock = request.Stock;
        product.Price = request.Price;
        await _productWriteRepository.SaveAsync();
        return new();
    }
}