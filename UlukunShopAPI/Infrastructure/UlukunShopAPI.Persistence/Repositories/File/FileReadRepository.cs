using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.File;

public class FileReadRepository:ReadRepository<Domain.Entities.File>,IFileReadRepository
{
    public FileReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}