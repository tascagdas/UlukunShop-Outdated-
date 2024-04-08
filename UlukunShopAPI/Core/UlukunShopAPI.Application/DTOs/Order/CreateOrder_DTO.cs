namespace UlukunShopAPI.Application.DTOs.Order;

public class CreateOrder_DTO
{
    public string? BasketId { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}