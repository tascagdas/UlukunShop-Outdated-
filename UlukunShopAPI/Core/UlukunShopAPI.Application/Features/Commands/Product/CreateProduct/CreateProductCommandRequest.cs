using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandRequest:IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}