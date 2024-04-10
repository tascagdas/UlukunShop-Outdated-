using UlukunShopAPI.Application.DTOs.Order;
using UlukunShopAPI.Domain.Entities;

namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrder_DTO createOrderDto);
    Task<ListOrder_DTO> GetAllOrdersAsync(int page, int size);
    Task<SingleOrder> GetOrderByIdAsync(string id);
    Task<(bool, CompletedOrder_DTO)> CompleteOrderAsync(string id);
}