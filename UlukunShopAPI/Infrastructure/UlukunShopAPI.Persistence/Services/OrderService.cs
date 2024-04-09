using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.Order;
using UlukunShopAPI.Application.Repositories;

namespace UlukunShopAPI.Persistence.Services;

public class OrderService:IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    readonly IOrderReadRepository _orderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
    }

    public async Task CreateOrderAsync(CreateOrder_DTO createOrderDto)
    {
        var orderCode = (new Random().NextDouble() * 10000).ToString();
        orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);
        
        await _orderWriteRepository.AddAsync(new()
        {
            Address = createOrderDto.Address,
            Id = Guid.Parse(createOrderDto.BasketId),
            Description = createOrderDto.Description,
            OrderCode = orderCode
        });
        await _orderWriteRepository.SaveAsync();
    }
    
    public async Task<ListOrder_DTO> GetAllOrdersAsync(int page, int size)
    {
        var query = _orderReadRepository.Table.Include(o => o.ShoppingCart)
            .ThenInclude(b => b.User)
            .Include(o => o.ShoppingCart)
            .ThenInclude(b => b.ShoppingCartItems)
            .ThenInclude(bi => bi.Product);

        var data = query.Skip(page * size).Take(size);
        /*.Take((page * size)..size);*/

        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data.Select(o => new
            {
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.ShoppingCart.ShoppingCartItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserName = o.ShoppingCart.User.UserName
            }).ToListAsync()
        };
    }
    
}