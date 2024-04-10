using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Domain.Entities;

public class Order:BaseEntity
{
    // public Guid CustomerId { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    // public ICollection<Product> Products { get; set; }
    // public Customer Customer { get; set; }
    public Guid ShoppingCartId { get; set; }
    public string OrderCode { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public CompletedOrder CompletedOrder { get; set; }
}