using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.Order;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.CompletedOrder;
using UlukunShopAPI.Domain.Entities;

namespace UlukunShopAPI.Persistence.Services;

public class OrderService:IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    readonly IOrderReadRepository _orderReadRepository;
    readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
    readonly ICompletedOrderReadRepository _completedOrderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _completedOrderWriteRepository = completedOrderWriteRepository;
        _completedOrderReadRepository = completedOrderReadRepository;
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


        var data2 = from order in data
            join completedOrder in _completedOrderReadRepository.Table
                on order.Id equals completedOrder.OrderId into co
            from _co in co.DefaultIfEmpty()
            select new
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                OrderCode = order.OrderCode,
                Basket = order.ShoppingCart,
                Completed = _co != null ? true : false
            };

        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data2.Select(o => new
            {
                Id = o.Id,
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.Basket.ShoppingCartItems.Sum(bi => (double)bi.Product.Price * bi.Quantity),
                UserName = o.Basket.User.UserName,
                o.Completed
            }).ToListAsync()
        };
    }
    
    public async Task<SingleOrder> GetOrderByIdAsync(string id)
    {
        var data = _orderReadRepository.Table
            .Include(o => o.ShoppingCart)
            .ThenInclude(b => b.ShoppingCartItems)
            .ThenInclude(bi => bi.Product);

        var data2 = await (from order in data
            join completedOrder in _completedOrderReadRepository.Table
                on order.Id equals completedOrder.OrderId into co
            from _co in co.DefaultIfEmpty()
            select new
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                OrderCode = order.OrderCode,
                Basket = order.ShoppingCart,
                Completed = _co != null ? true : false,
                Address = order.Address,
                Description = order.Description
            }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        return new()
        {
            Id = data2.Id.ToString(),
            BasketItems = data2.Basket.ShoppingCartItems.Select(bi => new
            {
                bi.Product.Name,
                bi.Product.Price,
                bi.Quantity
            }),
            Address = data2.Address,
            CreatedDate = data2.CreatedDate,
            Description = data2.Description,
            OrderCode = data2.OrderCode,
            Completed = data2.Completed
        };
    }
    
    public async Task<(bool, CompletedOrder_DTO)> CompleteOrderAsync(string id)
    {
        Order? order = await _orderReadRepository.Table
            .Include(o => o.ShoppingCart)
            .ThenInclude(sc => sc.User)
            .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
        if (order != null)
        {
            await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
            return (await _completedOrderWriteRepository.SaveAsync() > 0, new()
            {
                OrderCode = order.OrderCode,
                OrderDate = order.CreatedDate,
                Username = order.ShoppingCart.User.UserName,
                EMail = order.ShoppingCart.User.Email
            });
        }
        return (false, null);
    }

    
}