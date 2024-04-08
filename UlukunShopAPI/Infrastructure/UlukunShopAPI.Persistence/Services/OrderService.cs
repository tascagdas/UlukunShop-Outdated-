using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.Order;
using UlukunShopAPI.Application.Repositories;

namespace UlukunShopAPI.Persistence.Services;

public class OrderService:IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository)
    {
        _orderWriteRepository = orderWriteRepository;
    }

    public async Task CreateOrderAsync(CreateOrder_DTO createOrderDto)
    {
        await _orderWriteRepository.AddAsync(new()
        {
            Address = createOrderDto.Address,
            Id = Guid.Parse(createOrderDto.BasketId),
            Description = createOrderDto.Description
        });
        await _orderWriteRepository.SaveAsync();
    }
}