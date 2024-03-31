using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandRequest:IRequest<UpdateProductCommandResponse>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}