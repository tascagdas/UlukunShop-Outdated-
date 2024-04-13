using UlukunShopAPI.Domain.Entities.Common;
using UlukunShopAPI.Domain.Entities.Identity;

namespace UlukunShopAPI.Domain.Entities;

public class Menu : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Endpoint> Endpoints { get; set; }
}