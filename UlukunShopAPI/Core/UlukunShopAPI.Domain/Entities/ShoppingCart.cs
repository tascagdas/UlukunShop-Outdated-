using UlukunShopAPI.Domain.Entities.Common;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Domain.Entities;

public class ShoppingCart:BaseEntity
{
    public string UserId { get; set; }

    public AppUser User { get; set; }
    public Order Order { get; set; }
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}