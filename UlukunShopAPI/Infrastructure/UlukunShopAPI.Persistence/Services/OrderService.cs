using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.Order;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;

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
                Id=o.Id,
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.ShoppingCart.ShoppingCartItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserName = o.ShoppingCart.User.UserName,
            }).ToListAsync()
        };
    }
    
    public async Task<SingleOrder> GetOrderByIdAsync(string id)
    {
        var data = await _orderReadRepository.Table
            .Include(o => o.ShoppingCart)
            .ThenInclude(b => b.ShoppingCartItems)
            .ThenInclude(bi => bi.Product)
            .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        return new()
        {
            Id = data.Id.ToString(),
            BasketItems = data.ShoppingCart.ShoppingCartItems.Select(bi => new
            {
                bi.Product.Name,
                bi.Product.Price,
                bi.Quantity
            }),
            Address = data.Address,
            CreatedDate = data.CreatedDate,
            Description = data.Description,
            OrderCode = data.OrderCode
        };
    }
    
}