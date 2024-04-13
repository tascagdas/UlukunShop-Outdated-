using UlukunShopAPI.Application.Repositories.Menu;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.Menu;

public class MenuReadRepository : ReadRepository<Domain.Entities.Menu>, IMenuReadRepository
{
    public MenuReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}