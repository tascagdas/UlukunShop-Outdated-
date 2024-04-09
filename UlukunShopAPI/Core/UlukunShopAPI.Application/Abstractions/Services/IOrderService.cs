using UlukunShopAPI.Application.DTOs.Order;

namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrder_DTO createOrderDto);
    Task<ListOrder_DTO> GetAllOrdersAsync(int page, int size);
}