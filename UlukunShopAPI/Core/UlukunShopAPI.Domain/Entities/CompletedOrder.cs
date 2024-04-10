using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Domain.Entities;

public class CompletedOrder : BaseEntity
{
    public Guid OrderId { get; set; }

    public Order Order { get; set; }
}