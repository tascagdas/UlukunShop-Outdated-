using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.File;

public class FileWriteRepository:WriteRepository<Domain.Entities.File>,IFileWriteRepository
{
    public FileWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}