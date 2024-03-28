using UlukunShopAPI.Application.Repositories.InvoiceFile;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories.InvoiceFile;

public class InvoiceFileReadRepository:ReadRepository<Domain.Entities.InvoiceFile>,IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(UlukunAPIDbContext context) : base(context)
    {
    }
}