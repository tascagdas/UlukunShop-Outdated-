using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Domain.Entities;

public class Customer:BaseEntity
{
    public string Name { get; set; }
    // public ICollection<Order> Orders { get; set; }
    
}