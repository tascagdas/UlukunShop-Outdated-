using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Domain.Entities;

public class Product:BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public string? Properties { get; set; }
    // public ICollection<Order> Orders { get; set; }
    public ICollection<ProductImageFile> ProductImages { get; set; }
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}