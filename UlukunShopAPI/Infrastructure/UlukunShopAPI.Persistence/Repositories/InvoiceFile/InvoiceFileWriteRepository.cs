using UlukunShopAPI.Application.Repositories.InvoiceFile;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.InvoiceFile;

public class InvoiceFileWriteRepository:WriteRepository<Domain.Entities.InvoiceFile>,IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}