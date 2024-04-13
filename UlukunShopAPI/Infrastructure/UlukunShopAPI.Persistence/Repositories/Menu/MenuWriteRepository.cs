using UlukunShopAPI.Application.Repositories.Menu;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.Menu;

public class MenuWriteRepository : WriteRepository<Domain.Entities.Menu>, IMenuWriteRepository
{
    public MenuWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}